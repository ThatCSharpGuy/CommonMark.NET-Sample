using System;
using System.IO;
using CommonMark;

namespace CommonMarkNETSample
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			// String 
			var file = File.ReadAllText("input.md");
			var exp = CommonMark.CommonMarkConverter.Convert(file);
			Console.WriteLine(exp);

			// Stream
			using (var reader = new StreamReader("input.md"))
			using (var writer = new StreamWriter("output.html"))
			{
				CommonMark.CommonMarkConverter.Convert(reader, writer);
			}



			// Personalizando el resultado
			using (var reader = new StreamReader("input.md"))
			using (var writer = new StreamWriter("output1.html"))
			{
				var settings = CommonMarkSettings.Default.Clone();
				settings.OutputDelegate += (doc, output, sett) =>
				new CustomHtmlFormatter(output, sett).WriteDocument(doc);

				CommonMark.CommonMarkConverter.Convert(reader, writer, settings);
			}
		}

	}
}
