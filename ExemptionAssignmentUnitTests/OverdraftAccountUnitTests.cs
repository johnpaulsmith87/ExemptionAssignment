using ExemptionAssignment.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExemptionAssignmentUnitTests
{
    [TestClass]
    public class OverdraftAccountUnitTests
    {
        [TestMethod]
        public void OverdraftAccount_Debit_NotOverdraft()
        {
            decimal debitAmount = 50;
            decimal initalBalance = 100;
            decimal expected = 50;
            decimal overdraftLimit = 100;
            PrivateCustomer pc = new PrivateCustomer()
            {
                CustomerID = Guid.NewGuid(),
                ContactInformation = new Contact()
                {
                    FirstName = "test",
                    LastName = "test",
                    Address = "test",
                    Email = "test",
                    PhoneNumbers = new List<string>() { "123-122" }
                }
            };
            OverdraftAccount ba = new OverdraftAccount(initalBalance, new List<PrivateCustomer>() { pc }, overdraftLimit);

            ba.Debit(debitAmount);

            decimal actual = ba.Balance;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void OverdraftAccount_Debit_Overdraft()
        {
            decimal debitAmount = 150;
            decimal initalBalance = 100;
            decimal expected = -50;
            decimal overdraftLimit = 10000;
            PrivateCustomer pc = new PrivateCustomer()
            {
                CustomerID = Guid.NewGuid(),
                ContactInformation = new Contact()
                {
                    FirstName = "test",
                    LastName = "test",
                    Address = "test",
                    Email = "test",
                    PhoneNumbers = new List<string>() { "123-122" }
                }
            };
            OverdraftAccount ba = new OverdraftAccount(initalBalance, new List<PrivateCustomer>() { pc }, overdraftLimit);

            ba.Debit(debitAmount);

            decimal actual = ba.Balance;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void OverdraftAccount_Debit_ExceedLimit()
        {
            decimal debitAmount = 250;
            decimal initalBalance = 100;
            decimal expected = 100;
            decimal overdraftLimit = 100;
            PrivateCustomer pc = new PrivateCustomer()
            {
                CustomerID = Guid.NewGuid(),
                ContactInformation = new Contact()
                {
                    FirstName = "test",
                    LastName = "test",
                    Address = "test",
                    Email = "test",
                    PhoneNumbers = new List<string>() { "123-122" }
                }
            };
            OverdraftAccount ba = new OverdraftAccount(initalBalance, new List<PrivateCustomer>() { pc }, overdraftLimit);

            ba.Debit(debitAmount);

            decimal actual = ba.Balance;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void Overdraft_Debit_InvalidAmount()
        {
            decimal debitAmount = -5;
            decimal initalBalance = 100;
            decimal expected = 100;
            decimal overdraftLimit = 10000;
            PrivateCustomer pc = new PrivateCustomer()
            {
                CustomerID = Guid.NewGuid(),
                ContactInformation = new Contact()
                {
                    FirstName = "test",
                    LastName = "test",
                    Address = "test",
                    Email = "test",
                    PhoneNumbers = new List<string>() { "123-122" }
                }
            };
            OverdraftAccount ba = new OverdraftAccount(initalBalance, new List<PrivateCustomer>() { pc }, overdraftLimit);

            ba.Debit(debitAmount);

            decimal actual = ba.Balance;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void OverdraftAccount_Credit_ValidAmount()
        {
            decimal creditAmount = 50;
            decimal initalBalance = 100;
            decimal expected = 150;
            decimal overdraftLimit = 10000;
            PrivateCustomer pc = new PrivateCustomer()
            {
                CustomerID = Guid.NewGuid(),
                ContactInformation = new Contact()
                {
                    FirstName = "test",
                    LastName = "test",
                    Address = "test",
                    Email = "test",
                    PhoneNumbers = new List<string>() { "123-122" }
                }
            };
            OverdraftAccount ba = new OverdraftAccount(initalBalance, new List<PrivateCustomer>() { pc }, overdraftLimit);

            ba.Credit(creditAmount);

            decimal actual = ba.Balance;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void OverdraftAccount_Credit_InvalidAmount()
        {
            decimal creditAmount = -50;
            decimal initalBalance = 100;
            decimal expected = 100;
            decimal overdraftLimit = 10000;
            PrivateCustomer pc = new PrivateCustomer()
            {
                CustomerID = Guid.NewGuid(),
                ContactInformation = new Contact()
                {
                    FirstName = "test",
                    LastName = "test",
                    Address = "test",
                    Email = "test",
                    PhoneNumbers = new List<string>() { "123-122" }
                }
            };
            OverdraftAccount ba = new OverdraftAccount(initalBalance, new List<PrivateCustomer>() { pc }, overdraftLimit);

            ba.Credit(creditAmount);

            decimal actual = ba.Balance;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void OverdraftAccount_InterestCalc_NoOverdraft()
        {
            decimal initalBalance = 100;
            decimal expected = 103;
            decimal overdraftLimit = 10000;
            PrivateCustomer pc = new PrivateCustomer()
            {
                CustomerID = Guid.NewGuid(),
                ContactInformation = new Contact()
                {
                    FirstName = "test",
                    LastName = "test",
                    Address = "test",
                    Email = "test",
                    PhoneNumbers = new List<string>() { "123-122" }
                }
            };
            OverdraftAccount ba = new OverdraftAccount(initalBalance, new List<PrivateCustomer>() { pc }, overdraftLimit);

            ba.CalculateInterest();

            decimal actual = ba.Balance;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void OverdraftAccount_InterestCalc_Overdraft()
        {
            decimal initalBalance = -100;
            decimal expected = -103.25M;
            decimal overdraftLimit = 10000;
            PrivateCustomer pc = new PrivateCustomer()
            {
                CustomerID = Guid.NewGuid(),
                ContactInformation = new Contact()
                {
                    FirstName = "test",
                    LastName = "test",
                    Address = "test",
                    Email = "test",
                    PhoneNumbers = new List<string>() { "123-122" }
                }
            };
            OverdraftAccount ba = new OverdraftAccount(initalBalance, new List<PrivateCustomer>() { pc }, overdraftLimit);

            ba.CalculateInterest();

            decimal actual = ba.Balance;
            Assert.AreEqual(expected, actual);
        }

    }
}
