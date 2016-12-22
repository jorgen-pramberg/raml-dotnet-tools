using System;
using System.IO;
using Microsoft.VisualStudio.TextTemplating;
using Raml.Parser.Expressions;
using Raml.Tools.ClientGenerator;

namespace MuleSoft.RAMLGen
{
    public class RamlClientGenerator
    {
        public RamlClientGenerator(RamlDocument ramlDoc, string rootNamespace)
        {
            model = new ClientGeneratorService( ramlDoc, "Client", rootNamespace ).BuildModel();
        }

        private readonly ClientGeneratorModel model;
        private readonly string rootNamespace;

        public void Generate(string targetFilePath, string templateFilePath)
        {
            var engine = new Engine();
            var host = new CustomHost();

            // Read the T4 from disk into memory
            var templateFileContent = File.ReadAllText(templateFilePath);

            host.TemplateFile = templateFilePath;
            host.Session = host.CreateSession();
            host.Session["model"] = model;
            host.Session["fileName"] = targetFilePath;
            Environment.SetEnvironmentVariable("HOMEPATH", Directory.GetCurrentDirectory());

            // This output can be used as a communication means as it is not 
            // saved by default when we run the session in our own host.
            var output = engine.ProcessTemplate(templateFileContent, host);

            if (host.Errors.HasErrors)
            {
                throw new Exception("");
            }

//            File.WriteAllText(targetFilePath, output, host.FileEncoding);
        }
    }
}