using System.Collections.Generic;
using Moq;
using Xunit;

namespace Uno.Tests
{
    public class PartieTest
    {
        private Mock<IPile> pileMock;

        private Partie partie;

        public PartieTest()
        {
            pileMock = new Mock<IPile>();

            partie = new Partie(pileMock.Object);
        }
    }
}