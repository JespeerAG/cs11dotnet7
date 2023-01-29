// See https://aka.ms/new-console-template for more information
using System.Xml;
using static System.Environment;
using static System.IO.Path;
using Streams;

SectionTitle("Writing to text streams");

string textFile = Combine(CurrentDirectory, "streams.txt");

StreamWriter text = File.CreateText(textFile);


foreach (string item in Viper.Callsigns)
{
    text.WriteLine(item);
}
text.Close();

WriteLine("{0} contains {1:N0} bytes.", textFile, new FileInfo(textFile).Length);

WriteLine(File.ReadAllText(textFile));

WriteLine();
SectionTitle("Writing to XML streams");

string xmlFile = Combine(CurrentDirectory, "streams.xml");

FileStream? xmlFileStream = null;
XmlWriter? xml = null;

try
{
    xmlFileStream = File.Create(xmlFile);

    xml = XmlWriter.Create(xmlFileStream, new XmlWriterSettings { Indent = true });

    xml.WriteStartDocument(); // Write the XML declaration

    xml.WriteStartElement("callsigns");

    foreach (string item in Viper.Callsigns)
    {
        xml.WriteElementString("callsign", item);
    }

    xml.WriteEndElement();

    xml.Close();
    xmlFileStream.Close();
}
catch (Exception ex)
{
    WriteLine($"{ex.GetType()} says {ex.Message}");
}
finally
{
    if(xml != null)
    {
        xml.Dispose();
        WriteLine("The XML writer's unmanaged resources have been disposed.");
    }

    if (xmlFileStream != null)
    {
        xmlFileStream.Dispose();
        WriteLine("The file stream's unmanaged resources have been disposed.");
    }
}

WriteLine($"{xmlFile} contains {new FileInfo(xmlFile).Length: N0} bytes");
WriteLine(File.ReadAllText(xmlFile));

WriteLine();
SectionTitle("Alternative way via using statement");

using (FileStream file2 = File.OpenWrite(Path.Combine(CurrentDirectory, "file2.txt")))
{
    using (StreamWriter writer2 = new StreamWriter(file2))
    {
        try
        {
            writer2.WriteLine("Welcome, .NET!");
        }
        catch (Exception ex)
        {
            WriteLine($"{ex.GetType()} says {ex.Message}");
        }
    } // Automatically calls dispose if writer 2 is not null
} // Automatically calls dispose if file2 is not null

// The above can be simplified without indentation and braces:
// using FileStream file2 = File.OpenWrite(Path.Combine(path, "file2.txt"));
// using StreamWriter writer2 = new(file2);

WriteLine();
SectionTitle("Compressing Streams");
Compress(algorithm: "gzip");
Compress(algorithm: "brotli");