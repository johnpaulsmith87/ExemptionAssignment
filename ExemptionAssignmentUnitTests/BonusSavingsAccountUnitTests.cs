﻿using ExemptionAssignment.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExemptionAssignmentUnitTests
{
    [TestClass]
    public class BonusSavingsAccountUnitTests
    {
        [TestMethod]
        public void SavingsAccount_Debit_Valid()
        {
            decimal debitAmount = 50;
            decimal initalBalance = 100;
            decimal expected = 50;
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
            BonusSavingsAccount ba = new BonusSavingsAccount(new List<PrivateCustomer>() { pc }, initalBalance);

            ba.Debit(debitAmount);

            decimal actual = ba.Balance;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void SavingsAccount_Debit_ExceedLimit()
        {
            decimal debitAmount = 250;
            decimal initalBalance = 100;
            decimal expected = 100;
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
            BonusSavingsAccount ba = new BonusSavingsAccount(new List<PrivateCustomer>() { pc }, initalBalance);

            ba.Debit(debitAmount);

            decimal actual = ba.Balance;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void SavingsAccount_Debit_InvalidAmount()
        {
            decimal debitAmount = -5;
            decimal initalBalance = 100;
            decimal expected = 100;
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
            BonusSavingsAccount ba = new BonusSavingsAccount(new List<PrivateCustomer>() { pc }, initalBalance);

            ba.Debit(debitAmount);

            decimal actual = ba.Balance;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void SavingsAccount_Credit_ValidAmount()
        {
            decimal creditAmount = 50;
            decimal initalBalance = 100;
            decimal expected = 150;
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
            BonusSavingsAccount ba = new BonusSavingsAccount(new List<PrivateCustomer>() { pc }, initalBalance);

            ba.Credit(creditAmount);

            decimal actual = ba.Balance;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void SavingsAccount_Credit_InvalidAmount()
        {
            decimal creditAmount = -50;
            decimal initalBalance = 100;
            decimal expected = 100;
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
            BonusSavingsAccount ba = new BonusSavingsAccount(new List<PrivateCustomer>() { pc }, initalBalance);

            ba.Credit(creditAmount);

            decimal actual = ba.Balance;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void SavingsAccount_InterestCalc_LastDebitMoreThanThirty()
        {
            decimal initalBalance = 100;
            decimal expected = 105.25M;
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
            BonusSavingsAccount ba = new BonusSavingsAccount(new List<PrivateCustomer>() { pc }, initalBalance);
            ba.LastDebit = default(DateTime);

            ba.CalculateInterest();

            decimal actual = ba.Balance;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void SavingsAccount_InterestCalc_LastDebitLessThanThirty()
        {
            decimal initalBalance = 100;
            decimal expected = 100;
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
            BonusSavingsAccount ba = new BonusSavingsAccount(new List<PrivateCustomer>() { pc }, initalBalance);

            ba.CalculateInterest();

            decimal actual = ba.Balance;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void SavingsAccount_ResetDebit()
        {
            decimal initalBalance = 100;
            DateTime expected = default(DateTime);
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
            BonusSavingsAccount ba = new BonusSavingsAccount(new List<PrivateCustomer>() { pc }, initalBalance);

            ba.ResetDebitCounter();

            DateTime actual = ba.LastDebit;
            Assert.AreEqual(expected, actual);
        }

    }
}
