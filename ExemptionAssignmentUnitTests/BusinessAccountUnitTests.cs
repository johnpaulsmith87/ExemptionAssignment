using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExemptionAssignment.Models;
using System.Collections.Generic;

namespace ExemptionAssignmentUnitTests
{
    /* Used https://msdn.microsoft.com/en-us/library/ms182532.aspx as the basis for this section */
    [TestClass]
    public class BusinessAccountUnitTests
    {
        [TestMethod]
        public void BusinessAccount_Debit_NotOverdraft()
        {
            decimal debitAmount = 50;
            decimal initalBalance = 100;
            decimal expected = 50;
            decimal overdraftLimit = 10000;
            BusinessCustomer bc = new BusinessCustomer()
            {
                CustomerID = Guid.NewGuid(),
                ContactInformation = new Contact()
                {
                    FirstName = "test",
                    LastName = "test",
                    Address = "123 test street",
                    Email = "test@test.com",
                    PhoneNumbers = new List<string>() { "123-122" }
                },
                RegisteredName = "Acme Inc",
                TradingName = "ACE"
            };
            BusinessAccount ba = new BusinessAccount(bc, initalBalance, overdraftLimit);

            ba.Debit(debitAmount);

            decimal actual = ba.Balance;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void BusinessAccount_Debit_Overdraft()
        {
            decimal debitAmount = 150;
            decimal initalBalance = 100;
            decimal expected = -50;
            decimal overdraftLimit = 10000;
            BusinessCustomer bc = new BusinessCustomer()
            {
                CustomerID = Guid.NewGuid(),
                ContactInformation = new Contact()
                {
                    FirstName = "test",
                    LastName = "test",
                    Address = "123 test street",
                    Email = "test@test.com",
                    PhoneNumbers = new List<string>() { "123-122" }
                },
                RegisteredName = "Acme Inc",
                TradingName = "ACE"
            };
            BusinessAccount ba = new BusinessAccount(bc, initalBalance, overdraftLimit);

            ba.Debit(debitAmount);

            decimal actual = ba.Balance;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void BusinessAccount_Debit_ExceedLimit()
        {
            decimal debitAmount = 250;
            decimal initalBalance = 100;
            decimal expected = 100;
            decimal overdraftLimit = 100;
            BusinessCustomer bc = new BusinessCustomer()
            {
                CustomerID = Guid.NewGuid(),
                ContactInformation = new Contact()
                {
                    FirstName = "test",
                    LastName = "test",
                    Address = "123 test street",
                    Email = "test@test.com",
                    PhoneNumbers = new List<string>() { "123-122" }
                },
                RegisteredName = "Acme Inc",
                TradingName = "ACE"
            };
            BusinessAccount ba = new BusinessAccount(bc, initalBalance, overdraftLimit);

            ba.Debit(debitAmount);

            decimal actual = ba.Balance;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void Business_Debit_InvalidAmount()
        {
            decimal debitAmount = -5;
            decimal initalBalance = 100;
            decimal expected = 100;
            decimal overdraftLimit = 10000;
            BusinessCustomer bc = new BusinessCustomer()
            {
                CustomerID = Guid.NewGuid(),
                ContactInformation = new Contact()
                {
                    FirstName = "test",
                    LastName = "test",
                    Address = "123 test street",
                    Email = "test@test.com",
                    PhoneNumbers = new List<string>() { "123-122" }
                },
                RegisteredName = "Acme Inc",
                TradingName = "ACE"
            };
            BusinessAccount ba = new BusinessAccount(bc, initalBalance, overdraftLimit);

            ba.Debit(debitAmount);

            decimal actual = ba.Balance;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void BusinessAccount_Credit_ValidAmount()
        {
            decimal creditAmount = 50;
            decimal initalBalance = 100;
            decimal expected = 150;
            decimal overdraftLimit = 10000;
            BusinessCustomer bc = new BusinessCustomer()
            {
                CustomerID = Guid.NewGuid(),
                ContactInformation = new Contact()
                {
                    FirstName = "test",
                    LastName = "test",
                    Address = "123 test street",
                    Email = "test@test.com",
                    PhoneNumbers = new List<string>() { "123-122" }
                },
                RegisteredName = "Acme Inc",
                TradingName = "ACE"
            };
            BusinessAccount ba = new BusinessAccount(bc, initalBalance, overdraftLimit);

            ba.Credit(creditAmount);

            decimal actual = ba.Balance;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void BusinessAccount_Credit_InvalidAmount()
        {
            decimal creditAmount = -50;
            decimal initalBalance = 100;
            decimal expected = 100;
            decimal overdraftLimit = 10000;
            BusinessCustomer bc = new BusinessCustomer()
            {
                CustomerID = Guid.NewGuid(),
                ContactInformation = new Contact()
                {
                    FirstName = "test",
                    LastName = "test",
                    Address = "123 test street",
                    Email = "test@test.com",
                    PhoneNumbers = new List<string>() { "123-122" }
                },
                RegisteredName = "Acme Inc",
                TradingName = "ACE"
            };
            BusinessAccount ba = new BusinessAccount(bc, initalBalance, overdraftLimit);

            ba.Credit(creditAmount);

            decimal actual = ba.Balance;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void BusinessAccount_InterestCalc_NoOverdraft()
        {
            decimal initalBalance = 100;
            decimal expected = 103;
            decimal overdraftLimit = 10000;
            BusinessCustomer bc = new BusinessCustomer()
            {
                CustomerID = Guid.NewGuid(),
                ContactInformation = new Contact()
                {
                    FirstName = "test",
                    LastName = "test",
                    Address = "123 test street",
                    Email = "test@test.com",
                    PhoneNumbers = new List<string>() { "123-122" }
                },
                RegisteredName = "Acme Inc",
                TradingName = "ACE"
            };
            BusinessAccount ba = new BusinessAccount(bc, initalBalance, overdraftLimit);

            ba.CalculateInterest();

            decimal actual = ba.Balance;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void BusinessAccount_InterestCalc_Overdraft()
        {
            decimal initalBalance = -100;
            decimal expected = -103.25M;
            decimal overdraftLimit = 10000;
            BusinessCustomer bc = new BusinessCustomer()
            {
                CustomerID = Guid.NewGuid(),
                ContactInformation = new Contact()
                {
                    FirstName = "test",
                    LastName = "test",
                    Address = "123 test street",
                    Email = "test@test.com",
                    PhoneNumbers = new List<string>() { "123-122" }
                },
                RegisteredName = "Acme Inc",
                TradingName = "ACE"
            };
            BusinessAccount ba = new BusinessAccount(bc, initalBalance, overdraftLimit);

            ba.CalculateInterest();

            decimal actual = ba.Balance;
            Assert.AreEqual(expected, actual);
        }

    }
}
