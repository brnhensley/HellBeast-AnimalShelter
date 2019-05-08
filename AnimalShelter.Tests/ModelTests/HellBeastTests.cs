using Microsoft.VisualStudio.TestTools.UnitTesting;
using AnimalShelter.Models;
using AnimalShelterDatabase;
using System.Collections.Generic;
using System;

namespace AnimalShelter.Tests
{
  [TestClass]
  public class HellBeastTest : IDisposable
  {

    public void Dispose()
    {
      HellBeast.ClearAll();
    }

    public HellBeastTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=animal_shelter_test;";
    }

    [TestMethod]
    public void GetAll_ReturnsEmptyList_HellBeastList()
    {
      //Arrange
      List<HellBeast> newList = new List<HellBeast> { };

      //Act
      List<HellBeast> result = HellBeast.GetAll();

      //Assert
      CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
    public void GetAll_ReturnsHellBeasts_HellBeastList()
    {
      //Arrange
      HellBeast newHellBeast1 = new HellBeast("Cerebus", "Greece", "Gresham");
      newHellBeast1.Save();
      HellBeast newHellBeast2 = new HellBeast("Baal", "Egyptian", "Ohio");
      newHellBeast2.Save();
      List<HellBeast> expectedResult = new List<HellBeast> { newHellBeast1, newHellBeast2 };

      //Act
      List<HellBeast> actualResult = HellBeast.GetAll();

      //Assert
      CollectionAssert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    public void Equals_ReturnsTrueIfNameAreTheSame_HellBeast()
    {
      // Arrange, Act
      HellBeast firstHellBeast = new HellBeast("Baal", "Egyptian", "Ohio");
      HellBeast secondHellBeast = new HellBeast("Baal", "Egyptian", "Ohio");

      // Assert
      Assert.AreEqual(firstHellBeast, secondHellBeast);
    }

    [TestMethod]
    public void Save_SavesToDatabase_HellBeastList()
    {
      //Arrange
      HellBeast testHellBeast = new HellBeast("Baal", "Egyptian", "Ohio");
      testHellBeast.Save();

      //Act
      List<HellBeast> result = HellBeast.GetAll();
      List<HellBeast> testList = new List<HellBeast>{testHellBeast};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void Save_AssignsIdToObject_Id()
    {
      //Arrange
      HellBeast testHellBeast = new HellBeast("Baal", "Egyptian", "Ohio");

      //Act
      testHellBeast.Save();
      HellBeast savedHellBeast = HellBeast.GetAll()[0];

      int result = savedHellBeast.Id;
      int testId = testHellBeast.Id;

      //Assert
      Assert.AreEqual(testId, result);
    }

    [TestMethod]
    public void Find_ReturnsCorrectHellBeast_HellBeast()
    {
      //Arrange
      HellBeast testHellBeast = new HellBeast("Baal", "Egyptian", "Ohio");
      testHellBeast.Save();

      //Act
      HellBeast foundHellBeast = HellBeast.Find(testHellBeast.Id);

      //Assert
      Assert.AreEqual(testHellBeast, foundHellBeast);
    }

//      Adjust this 
    // [TestMethod]
    // public void Edit_UpdatesItemInDatabase_String()
    // {
    //   //Arrange
    //   string firstDescription = "Walk the Dog";
    //   Item testItem = new Item(firstDescription);
    //   testItem.Save();
    //   string secondDescription = "Mow the lawn";
    //
    //   //Act
    //   testItem.Edit(secondDescription);
    //   string result = Item.Find(testItem.GetId()).GetDescription();
    //
    //   //Assert
    //   Assert.AreEqual(secondDescription, result);
    // }


  }
}
