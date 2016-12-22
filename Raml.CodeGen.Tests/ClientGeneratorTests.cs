using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using MuleSoft.RAMLGen;
using NUnit.Framework;
using Raml.Parser;

namespace Raml.CodeGen.Tests
{
    [TestFixture]
    class ClientGeneratorTests
    {
        [Test]
        public void GenerateCode_WhenMovies()
        {
            var ramlFile = "files/raml1/movies-v1.raml";
            var fi = new FileInfo( ramlFile );
            var raml = new RamlParser().LoadAsync( fi.FullName );

            var clientGenerator = new RamlClientGenerator(raml.Result, "Movies");
            //clientGenerator.Generate( $@"{Directory.GetCurrentDirectory()}\Output\{{0}}Model.cs", @"Templates\Models.t4" );
            //clientGenerator.Generate( $@"{Directory.GetCurrentDirectory()}\Output\{{0}}Communication.cs", @"Templates\Communication.t4" );
            clientGenerator.Generate( @"Output\Header.cs", @"Templates\Header.t4" );
            //clientGenerator.Generate( @"Output\Request.cs", @"Templates\Request.t4" );
            //clientGenerator.Generate( @"Output\Response.cs", @"Templates\Response.t4" );
            //clientGenerator.Generate( @"Output\ResponseHeader.cs", @"Templates\ResponseHeader.t4" );
            //clientGenerator.Generate( @"Output\Types.cs", @"Templates\Types.t4" );
        }
    }
}
