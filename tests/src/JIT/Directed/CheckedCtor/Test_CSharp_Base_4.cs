// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

//
// This test represents a case where csc.exe puts a base/peer ctor callsite outside of the
// first block of the derived ctor.
//
// Specifically covers: "Instance field initializers preceding the base ctor callsite"
//

using System;
using System.Runtime.CompilerServices;

namespace Test
{
    static class App
    {
        static int Main()
        {
            new DerivedClass(7);
            return 100;
        }
    }

    public class BaseClass
    {
        [MethodImpl(MethodImplOptions.NoInlining)]
        public BaseClass(int arg) { Console.Write("BaseClass::.ctor -- `{0}'\r\n", arg); return; }
    }

    public class DerivedClass : BaseClass
    {
        private static readonly Random Generator = new Random();
        private static string GetString() { return "Text"; }
        public int Field1 = ((Generator.Next(5, 8) == 10) ? 10 : 20);
        public string Field2 = (GetString() ?? "NeededToFallBack");
        public Func<int> Field3 = () => Generator.Next(5, 8);

        [MethodImpl(MethodImplOptions.NoInlining)]
        public DerivedClass(int selector) : base(selector) { }
    }
}

