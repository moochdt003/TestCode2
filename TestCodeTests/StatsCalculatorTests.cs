using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestCode;

namespace TestCodeTests
{
    [TestClass]
    public class StatsCalculatorTests
    {
        [TestMethod]
        public void DummyTestThatWillNotRunAsWeHaveNoTestFrameworkHookedUp()
        {
            //Arrange
            var sut = new StatsCalculator(SampleData.Teams, new StatsWeightingStub());

            //Act
            var result = sut.PlayerByPlayerNumber(5);
            //Do assertions that result is null here i.e.result.ShouldBeNull() or Assert.IsNull(result).Choose what ever method your preferred test framework uses
            //Assert
            Assert.AreEqual("Michał Bąkiewicz", result.Name);
        }

        //[TestMethod]
        //public void LivingCell_HasZeroLivingNeighbours_Dies()
        //{
        //    //Arrange
        //    const int livingNeigbours = 0;

        //    //Act
        //    var result = GameOfLifeRules.ChangeCellState(true, livingNeigbours);

        //    //Assert
        //    Assert.AreEqual(false, result);
        //}
    }
}
