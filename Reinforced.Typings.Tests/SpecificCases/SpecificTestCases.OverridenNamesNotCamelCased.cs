﻿using Reinforced.Typings.Attributes;
using Reinforced.Typings.Fluent;
using Xunit;

namespace Reinforced.Typings.Tests.SpecificCases
{
    public class TestOverrides
    {
        [TsProperty(Name = "ID", ForceNullable = true)]
        public virtual int _id { get { return 0; } }

        [TsFunction(Name = "DO_DOMETHING")]
        public void DoSomething() { }

        public void AnotherMethod() { }

        public string AnotherProeprty { get; set; }
    }
    public partial class SpecificTestCases
    {


        [Fact]
        public void OverridenNamesNotCamelCased()
        {
            const string result = @"
module Reinforced.Typings.Tests.SpecificCases {
	export interface ITestOverrides
	{
		ID?: number;
		Another_Property: string;
		DO_DOMETHING() : void;
		Another_Method() : void;
	}
}";
            AssertConfiguration(s =>
            {
                s.Global(a => a.DontWriteWarningComment().CamelCaseForMethods().CamelCaseForProperties());
                s.ExportAsInterface<TestOverrides>()
                .WithPublicProperties()
                .WithPublicMethods()
                .WithMethod(x => x.AnotherMethod(), x => x.OverrideName("Another_Method"))
                .WithProperty(x => x.AnotherProeprty, x => x.OverrideName("Another_Property"))
                ;
            }, result);
        }
    }
}