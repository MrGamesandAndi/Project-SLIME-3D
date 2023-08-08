using System.Collections.Generic;

public class LocalisationSystem
{
	public enum Language
	{
		Spanish,
		English
	}

	#region Variables
	public static Language language = Language.Spanish;

	private static Dictionary<string, string> localisedES;
	private static Dictionary<string, string> localisedEN;

	public static bool isInit;
	public static CSVLoader csvLoader;
	#endregion

	#region Custom Methods
	public static void Init()
	{
		csvLoader = new CSVLoader();
		csvLoader.LoadCSV();
		UpdateDictionaries();
		isInit = true;
	}

	public static void UpdateDictionaries()
	{
		localisedES = csvLoader.GetDictionaryValues("es");
		localisedEN = csvLoader.GetDictionaryValues("en");
	}

	public static Dictionary<string, string> GetDictionaryForEditor()
	{
		if (!isInit)
		{
			Init();
		}

		return localisedES;
	}

	public static string GetLocalisedValue(string key)
	{
		if (!isInit)
		{
			Init();
		}

		string value = key;

		switch (language)
		{
			case Language.Spanish:
				localisedES.TryGetValue(key, out value);
				break;
			case Language.English:
				localisedEN.TryGetValue(key, out value);
				break;
			default:
				localisedES.TryGetValue(key, out value);
				break;
		}

		return value;
	}

	public static void SetLanguage(Language newLanguage)
	{
		language = newLanguage;
	}

#if UNITY_EDITOR
	public static void Add(string key, string value)
	{
		if (value.Contains("\""))
		{
			value.Replace('"', '\"');
		}

		if (csvLoader == null)
		{
			csvLoader = new CSVLoader();
		}

		csvLoader.LoadCSV();
		csvLoader.Add(key, value);
		csvLoader.LoadCSV();
		UpdateDictionaries();
	}

	public static void Replace(string key, string value)
	{
		if (value.Contains("\""))
		{
			value.Replace('"', '\"');
		}

		if (csvLoader == null)
		{
			csvLoader = new CSVLoader();
		}

		csvLoader.LoadCSV();
		csvLoader.Edit(key, value);
		csvLoader.LoadCSV();
		UpdateDictionaries();
	}

	public static void Remove(string key)
	{
		if (csvLoader == null)
		{
			csvLoader = new CSVLoader();
		}

		csvLoader.LoadCSV();
		csvLoader.Remove(key);
		csvLoader.LoadCSV();
		UpdateDictionaries();
	}
#endif
#endregion
}
