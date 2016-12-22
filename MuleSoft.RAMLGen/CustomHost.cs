using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.TextTemplating;

namespace MuleSoft.RAMLGen
{
    public class CustomHost : ITextTemplatingEngineHost, ITextTemplatingSessionHost
    {
        public ITextTemplatingSession Session { get; set; }

        public ITextTemplatingSession CreateSession()
        {
            if( Session == null )
                Session = new TextTemplatingSession();

            return Session;
        }

        public IList<string> StandardAssemblyReferences => new string[] {};

        public IList<string> StandardImports => new string[0];

        public string TemplateFile { get; set; }

        public object GetHostOption( string optionName )
        {
            return null;
        }

        public bool LoadIncludeText( string requestFileName, out string content, out string location )
        {
            content = File.ReadAllText(requestFileName);
            location = Path.GetFullPath(requestFileName);
            return true;
        }

        public void LogErrors( CompilerErrorCollection errors )
        {
            Errors = errors;
        }

        public CompilerErrorCollection Errors { get; private set; }

        public AppDomain ProvideTemplatingAppDomain( string content )
        {
            return AppDomain.CurrentDomain;
        }

        private readonly Dictionary<string, string> assemblyReferences =
            new Dictionary<string, string>
            {
                ["System.Core"] =
                @"C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.5.1\System.Core.dll",
                ["System.Runtime"] =
                @"C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.5.1\Facades\System.Runtime.dll",
                ["System"] =
                @"C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.5.1\System.dll"
            };


        public string ResolveAssemblyReference( string assemblyReference )
        {
            if( File.Exists( assemblyReference ) )
                return assemblyReference;

            var path = String.Empty;
            if (assemblyReferences.TryGetValue(assemblyReference, out path))
                return path;

            if(!Path.HasExtension(assemblyReference))
                path = Path.Combine(Directory.GetCurrentDirectory(), $"{assemblyReference}.dll");

            return path;
        }

        public Type ResolveDirectiveProcessor( string processorName )
        {
            throw new NotImplementedException();
        }

        public string ResolveParameterValue( string directiveId, string processorName, string parameterName )
        {
            throw new NotImplementedException();
        }

        public string ResolvePath( string path )
        {
            throw new NotImplementedException();
        }

        public void SetFileExtension( string extension )
        {
        }

        public void SetOutputEncoding( Encoding encoding, bool fromOutputDirective )
        {
            FileEncoding = encoding;
        }

        public Encoding FileEncoding { get; set; } = Encoding.UTF8;
    }
}
