﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Globalization;
using Xunit;

namespace System.Globalization.Tests
{
    public class TextElementEnumeratorCurrent
    {
        // PosTest1: Calling Current Property
        [Fact]
        public void PosTest1()
        {
            // Creates and initializes a String containing the following:
            //   - a surrogate pair (high surrogate U+D800 and low surrogate U+DC00)
            //   - a combining character sequence (the Latin small letter "a" followed by the combining grave accent)
            //   - a base character (the ligature "")
            String myString = "\uD800\uDC00\u0061\u0300\u00C6";
            string[] expectValue = new string[5];
            expectValue[0] = "\uD800\uDC00";
            expectValue[1] = "\uD800\uDC00";
            expectValue[2] = "\u0061\u0300";
            expectValue[3] = "\u0061\u0300";
            expectValue[4] = "\u00C6";
            // Creates and initializes a TextElementEnumerator for myString.
            TextElementEnumerator myTEE = StringInfo.GetTextElementEnumerator(myString);
            myTEE.Reset();
            string actualValue = null;
            while (myTEE.MoveNext())
            {
                actualValue = myTEE.Current.ToString();
                Assert.Equal(expectValue[myTEE.ElementIndex], actualValue);
            }
        }

        // NegTest1: The enumerator is positioned before the first text element of the string
        [Fact]
        public void NegTest1()
        {
            String myString = "\uD800\uDC00\u0061\u0300\u00C6";
            // Creates and initializes a TextElementEnumerator for myString.
            TextElementEnumerator myTEE = StringInfo.GetTextElementEnumerator(myString);
            myTEE.Reset();
            string actualValue = null;
            Assert.Throws<InvalidOperationException>(() =>
            {
                actualValue = myTEE.Current.ToString();
            });
        }

        // NegTest2: The enumerator is positioned  after the last text element
        [Fact]
        public void NegTest2()
        {
            String myString = "\uD800\uDC00\u0061\u0300\u00C6";

            // Creates and initializes a TextElementEnumerator for myString.
            TextElementEnumerator myTEE = StringInfo.GetTextElementEnumerator(myString);
            myTEE.Reset();
            string actualValue = null;
            while (myTEE.MoveNext())
            {
                actualValue = myTEE.Current.ToString();
            }
            Assert.Throws<InvalidOperationException>(() =>
            {
                actualValue = myTEE.Current.ToString();
            });
        }
    }
}

