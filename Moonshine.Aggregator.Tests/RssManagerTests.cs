﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Moonshine.Aggregator.Tests
{
    [TestClass]
    public class RssManagerTests
    {
        [TestMethod]
        public void Verify_Read_Method()
        {
            RssManager.Read(new Uri("http://www.lemonde.fr/rss/une.xml"));
        }
    }
}