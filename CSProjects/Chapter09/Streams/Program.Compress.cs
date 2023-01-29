using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO.Compression; //BrotliStream, GZipStream, CompressionMode
using System.Xml; // XmlWriter, XmlReader

using static System.Environment; // CurrentDirectory
using static System.IO.Path; // Combine
using Streams;


partial class Program
{
    static void Compress(string algorithm = "gzip")
    {
        string filePath = Combine(CurrentDirectory, $"streams.{algorithm}");

        FileStream file = File.Create(filePath);

        Stream compressor;
        if (algorithm == "gzip")
        {
            compressor = new GZipStream(file, CompressionMode.Compress);
        }
        else
        {
            compressor = new BrotliStream(file, CompressionMode.Compress);
        }

        using (compressor)
        {
            using (XmlWriter xml = XmlWriter.Create(compressor))
            {
                xml.WriteStartDocument();
                xml.WriteStartElement("callsigns");
                foreach (string item in Viper.Callsigns)
                {
                    xml.WriteElementString("callsign", item);
                }
            }
        } // Also closes the underlying GZip/Brotli-stream

        WriteLine($"{filePath} contains {new FileInfo(filePath).Length:0} bytes.");

        WriteLine($"The compressed contents:");
        WriteLine(File.ReadAllText(filePath));

        WriteLine("Reading the compressed XML file:");
        file = File.Open(filePath, FileMode.Open);
        Stream decompressor;
        if (algorithm == "gzip")
        {
            decompressor = new GZipStream(file, CompressionMode.Decompress);
        }
        else
        {
            decompressor = new BrotliStream(file, CompressionMode.Decompress);
        }

        using (decompressor)

        using (XmlReader reader = XmlReader.Create(decompressor))

        while (reader.Read())
        {
            if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "callsign"))
            {
                    reader.Read();
                    WriteLine($"{reader.Value}");
                }
        }
    }
}