﻿using System;
using System.Linq;
using System.Collections.Generic;
using Terraria.ModLoader;
using Terraria;

namespace AssortedCrazyThings
{
	public class ConfigurationSystem : ModSystem
	{
		//These assume no ILoadable across the whole mod has a duplicate name (contrary to what tml allows)
		public static Dictionary<string, ContentType> NonLoadedNames { get; private set; }
		public static Dictionary<ContentType, List<string>> NonLoadedNamesByType { get; private set; }

        public override void OnModLoad()
        {
			NonLoadedNames = new();
			NonLoadedNamesByType = new();

			//Debugging only
			var autoloadedContent = Mod.GetContent().ToList();
			var manuallyAddedTypes = new List<Type>();

			Type modType = Mod.GetType();
			foreach (Type type in Mod.Code.GetTypes().OrderBy(type => type.FullName, StringComparer.InvariantCulture))
			{
				//Mirror autoloading conditions
				if (type == modType) continue;
				if (type.IsAbstract) continue;
				if (type.ContainsGenericParameters) continue;
				if (type.GetConstructor(Array.Empty<Type>()) == null) continue; //Don't autoload things with no default constructor
				if (!typeof(ILoadable).IsAssignableFrom(type)) continue; //Don't autoload non-ILoadables

				var autoload = AutoloadAttribute.GetValue(type);

				if (autoload.NeedsAutoloading) continue; //Skip things that are autoloaded (this code runs after Autoload())

				if (!LoadSide(autoload.Side)) continue; //Skip things that shouldn't load on a particular side

				var content = ContentAttribute.GetValue(type);

				if (content.Ignore) continue; //Skip things tagged as non-autoloadable, yet shouldn't be loaded through here (loaded elsewhere)

                var reasons = FindContentFilterReasons(content.ContentType);
				var instance = (ILoadable)Activator.CreateInstance(type);

				if (reasons == ContentType.Always)
				{
					//No filters
					manuallyAddedTypes.Add(type);
					Mod.AddContent(instance);
					continue; //Don't do anything further
				}

				if (instance is ModType modTypeInstance)
                {
                    string name = modTypeInstance.Name;
                    NonLoadedNames.Add(name, reasons);

					if (!NonLoadedNamesByType.ContainsKey(reasons))
                    {
						NonLoadedNamesByType[reasons] = new List<string>();
					}

                    NonLoadedNamesByType[reasons].Add(name);
				}
			}
		}

        private static ContentType FindContentFilterReasons(ContentType contentType)
        {
            if (contentType == ContentType.Always)
            {
				//Skip checking if this is not filtered anyway
				return ContentType.Always;
			}

			//Bitwise "and" results in the overlap, representing the flags that caused the content to be filtered
			return AConfigurationConfig.Instance.FilterFlags & contentType;
		}

        public override void Unload()
        {
			NonLoadedNames?.Clear();
			NonLoadedNames = null;

			NonLoadedNamesByType?.Clear();
			NonLoadedNamesByType = null;
		}

		public static string ContentTypeToString(ContentType contentType)
        {
			if (!ExactlyOneFlagSet(contentType))
            {
				string concat = string.Empty;
				foreach (ContentType flag in Enum.GetValues(typeof(ContentType)))
                {
					if (flag != ContentType.Always && contentType.HasFlag(flag))
                    {
						concat += ContentTypeToString(flag) + "/";
					}
                }
				return concat[0..^1];
			}

            return contentType switch
            {
                ContentType.Always => string.Empty,
                ContentType.Bosses => "Bosses",
                ContentType.HostileNPCs => "Hostile NPCs",
                ContentType.FriendlyNPCs => "Friendly NPCs",
				ContentType.DroppedPets => "Dropped Pets",
				ContentType.BossConsolation => "Boss Consolation Items",
				_ => string.Empty,
            };
		}

		public static bool ExactlyOneFlagSet(ContentType contentType)
		{
			return Enum.IsDefined(typeof(ContentType), contentType);
		}

		internal static bool LoadSide(ModSide side) => side != (Main.dedServ ? ModSide.Client : ModSide.Server);
	}

	[Flags]
	public enum ContentType : byte
    {
		Always = 0 << 0,
		Bosses = 1 << 1,
		HostileNPCs = 1 << 2,
		FriendlyNPCs = 1 << 3,
		DroppedPets = 1 << 4,
		BossConsolation = 1 << 5,
	}

	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
	public sealed class ContentAttribute : Attribute
	{
		private static readonly ContentAttribute Default = new ContentAttribute(ContentType.Always);

		public ContentType ContentType { get; private set; }

		public bool Ignore { get; private set; }

		public ContentAttribute(ContentType contentType, bool ignore = false)
		{
			ContentType = contentType;
			Ignore = ignore;
		}

		public static ContentAttribute GetValue(Type type)
		{
			//Get all attributes on the type.
			object[] all = type.GetCustomAttributes(typeof(ContentAttribute), true);
			//The first should be the most derived attribute.
			var mostDerived = (ContentAttribute)all.FirstOrDefault();
			//If there were no declarations, then return default.
			return mostDerived ?? Default;
		}
	}
}
