using System;
using System.Linq;
using Uno;
using Uno.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace Uno.MsTests
{
    [TestClass]
    public class PartieTest
    {
        private Mock<IPile> pileMock;
        private readonly Partie partie;

        public PartieTest()
        {
            pileMock = new Mock<IPile>();

            partie = new Partie(pileMock.Object);
        }
    }
}