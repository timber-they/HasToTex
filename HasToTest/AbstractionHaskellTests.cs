using System.Collections.Generic;

using HasToTex.Model.Abstraction.Haskell;
using HasToTex.Model.Abstraction.Haskell.Statements;
using HasToTex.Model.Abstraction.Haskell.Statements.Collections;
using HasToTex.Model.Abstraction.Haskell.Types;
using HasToTex.Model.Exceptions;

using NUnit.Framework;


// ReSharper disable ObjectCreationAsStatement


namespace HasToTest
{
    public class AbstractionHaskellTests
    {
        [SetUp]
        public void Setup () {}

        [Test]
        public void FiniteList ()
        {
            var emptyCode  = "";
            var weirdCode1 = "1,2,3]";
            var weirdCode2 = "[1,2,3";
            var normalCode = "[1,2,3]";

            var normalElements = new List <Statement>
            {
                new Atomic ("1", "1"),
                new Atomic ("2", "2"),
                new Atomic ("3", "3")
            };

            var weirdElements = new List <Statement>
            {
                new Atomic ("4", "4")
            };

            Assert.Throws <InvalidCodeException> (() => new FiniteList (emptyCode, normalElements));
            Assert.Throws <InvalidCodeException> (() => new FiniteList (weirdCode1, normalElements));
            Assert.Throws <InvalidCodeException> (() => new FiniteList (weirdCode2, normalElements));
            Assert.Throws <InvalidCodeException> (() => new FiniteList (normalCode, weirdElements));
            Assert.DoesNotThrow (() => new FiniteList (normalCode, normalElements));
        }

        [Test]
        public void ListComprehension ()
        {
            var emptyCode  = "";
            var weirdCode1 = "x | x <- 1,2,3]]";
            var weirdCode2 = "[x | x <- [1,2,3";
            var weirdCode3 = "[1,2,3]";
            var normalCode = "[x | x <- [1,2,3], x > 2]";

            var normalResult = new Atomic ("x", "x");
            var weirdResult  = new Atomic ("y", "y");

            var normalAssignments = new List <Assignment>
            {
                new Assignment ("x <- [1,2,3]",
                                "x",
                                new FiniteList ("[1,2,3]",
                                                new List <Statement>
                                                {
                                                    new Atomic ("1", "1"),
                                                    new Atomic ("2", "2"),
                                                    new Atomic ("3", "3"),
                                                }))
            };
            var weirdAssignments = new List <Assignment>
            {
                new Assignment ("y <- z", "y", new Atomic ("z", "z"))
            };

            var normalPredicates = new List <Statement>
            {
                new Call ("x > 2",
                          ">",
                          new List <Statement>
                          {
                              new Atomic ("x", "x"),
                              new Atomic ("2", "2")
                          })
            };
            var weirdPredicates = new List <Statement>
            {
                new Atomic ("False", "False")
            };

            Assert.Throws <InvalidCodeException> (
                () => new ListComprehension (emptyCode, normalResult, normalAssignments, normalPredicates));
            Assert.Throws <InvalidCodeException> (
                () => new ListComprehension (weirdCode1, normalResult, normalAssignments, normalPredicates));
            Assert.Throws <InvalidCodeException> (
                () => new ListComprehension (weirdCode2, normalResult, normalAssignments, normalPredicates));
            Assert.Throws <InvalidCodeException> (
                () => new ListComprehension (weirdCode3, normalResult, normalAssignments, normalPredicates));
            Assert.Throws <InvalidCodeException> (
                () => new ListComprehension (normalCode, weirdResult, normalAssignments, normalPredicates));
            Assert.Throws <InvalidCodeException> (
                () => new ListComprehension (normalCode, normalResult, weirdAssignments, normalPredicates));
            Assert.Throws <InvalidCodeException> (
                () => new ListComprehension (normalCode, normalResult, normalAssignments, weirdPredicates));
            Assert.DoesNotThrow (
                () => new ListComprehension (normalCode, normalResult, normalAssignments, normalPredicates));
        }

        [Test]
        public void ListRange ()
        {
            var emptyCode   = "";
            var weirdCode1  = "1,2..3]";
            var weirdCode2  = "[1,2..3";
            var normalCode1 = "[1,3..7]";
            var normalCode2 = "[1..3]";

            var normalElements1 = new List <Statement>
            {
                new Atomic ("1", "1"),
                new Atomic ("3", "3")
            };
            var normalElements2 = new List <Statement>
            {
                new Atomic ("1", "1")
            };
            var weirdElements = new List <Statement>
            {
                new Atomic ("4", "4")
            };

            var normalLastElement1 = new Atomic ("7", "7");
            var normalLastElement2 = new Atomic ("3", "3");
            var weirdLastElement   = new Atomic ("4", "4");

            Assert.Throws <InvalidCodeException> (
                () => new ListRange (emptyCode, normalElements1, normalLastElement1));
            Assert.Throws <InvalidCodeException> (
                () => new ListRange (weirdCode1, normalElements1, normalLastElement1));
            Assert.Throws <InvalidCodeException> (
                () => new ListRange (weirdCode2, normalElements1, normalLastElement1));
            Assert.Throws <InvalidCodeException> (
                () => new ListRange (normalCode1, weirdElements, normalLastElement1));
            Assert.Throws <InvalidCodeException> (
                () => new ListRange (normalCode1, normalElements1, weirdLastElement));
            Assert.DoesNotThrow (
                () => new ListRange (normalCode1, normalElements1, normalLastElement1));
            Assert.DoesNotThrow (
                () => new ListRange (normalCode2, normalElements2, normalLastElement2));
        }

        [Test]
        public void Assignment ()
        {
            var emptyCode  = "";
            var weirdCode1 = "x = y";
            var weirdCode2 = "x < y";
            var normalCode = "x <- y";

            var normalName = "x";
            var weirdName  = "z";

            var normalValue = new Atomic ("y", "y");
            var weirdValue  = new Atomic ("z", "z");

            Assert.Throws <InvalidCodeException> (() => new Assignment (emptyCode, normalName, normalValue));
            Assert.Throws <InvalidCodeException> (() => new Assignment (weirdCode1, normalName, normalValue));
            Assert.Throws <InvalidCodeException> (() => new Assignment (weirdCode2, normalName, normalValue));
            Assert.Throws <InvalidCodeException> (() => new Assignment (normalCode, weirdName, normalValue));
            Assert.Throws <InvalidCodeException> (() => new Assignment (normalCode, normalName, weirdValue));
            Assert.DoesNotThrow (() => new Assignment (normalCode, normalName, normalValue));
        }

        [Test]
        public void Atomic ()
        {
            var emptyCode  = "";
            var normalCode = "x";

            var normalName = "x";
            var weirdName  = "y";

            Assert.Throws <InvalidCodeException> (() => new Atomic (emptyCode, normalName));
            Assert.Throws <InvalidCodeException> (() => new Atomic (normalCode, weirdName));
            Assert.DoesNotThrow (() => new Atomic (normalCode, normalName));
        }

        [Test]
        public void Call ()
        {
            var emptyCode  = "";
            var normalCode = "f x y";

            var normalName = "f";
            var weirdName  = "g";

            var normalParameters = new List <Statement>
            {
                new Atomic ("x", "x"),
                new Atomic ("y", "y")
            };
            var weirdParameters = new List <Statement>
            {
                new Atomic ("z", "z")
            };

            Assert.Throws <InvalidCodeException> (() => new Call (emptyCode, normalName, normalParameters));
            Assert.Throws <InvalidCodeException> (() => new Call (normalCode, weirdName, normalParameters));
            Assert.Throws <InvalidCodeException> (() => new Call (normalCode, normalName, weirdParameters));
            Assert.DoesNotThrow (() => new Call (normalCode, normalName, normalParameters));
        }

        [Test]
        public void Case ()
        {
            var emptyCode  = "";
            var weirdCode1 = "case x of\na > b\n_ - d";
            var weirdCode2 = "ase x of\na -> b\n_ -> d";
            var weirdCode3 = "case x o\na -> b\n_ -> d";
            var normalCode = "case x of\na -> b\n_ -> d";

            var normalCase = new Atomic ("x", "x");
            var weirdCase  = new Atomic ("y", "y");

            var normalPatterns = new List <Case.Pattern>
            {
                new Case.Pattern (new Atomic ("a", "a"), new Atomic ("b", "b")),
                new Case.Pattern (null, new Atomic ("d", "d"))
            };
            var weirdPatterns = new List <Case.Pattern>
            {
                new Case.Pattern (new Atomic ("g", "g"), new Atomic ("d", "d"))
            };

            Assert.Throws <InvalidCodeException> (() => new Case (emptyCode, normalCase, normalPatterns));
            Assert.Throws <InvalidCodeException> (() => new Case (weirdCode1, normalCase, normalPatterns));
            Assert.Throws <InvalidCodeException> (() => new Case (weirdCode2, normalCase, normalPatterns));
            Assert.Throws <InvalidCodeException> (() => new Case (weirdCode3, normalCase, normalPatterns));
            Assert.Throws <InvalidCodeException> (() => new Case (normalCode, weirdCase, normalPatterns));
            Assert.Throws <InvalidCodeException> (() => new Case (normalCode, normalCase, weirdPatterns));
            Assert.DoesNotThrow (() => new Case (normalCode, normalCase, normalPatterns));
        }

        [Test]
        public void Function ()
        {
            var emptyCode  = "";
            var weirdCode  = "f a b - c";
            var normalCode = "f a b = c";

            var normalName = "f";
            var weirdName  = "g";

            var normalParameters = new List <string> {"a", "b"};
            var weirdParameters  = new List <string> {"d"};

            var normalBody = new Atomic ("c", "c");
            var weirdBody  = new Atomic ("d", "d");

            Assert.Throws <InvalidCodeException> (
                () => new Function (emptyCode, normalName, normalParameters, normalBody));
            Assert.Throws <InvalidCodeException> (
                () => new Function (weirdCode, normalName, normalParameters, normalBody));
            Assert.Throws <InvalidCodeException> (
                () => new Function (normalCode, weirdName, normalParameters, normalBody));
            Assert.Throws <InvalidCodeException> (
                () => new Function (normalCode, normalName, weirdParameters, normalBody));
            Assert.Throws <InvalidCodeException> (
                () => new Function (normalCode, normalName, normalParameters, weirdBody));
            Assert.DoesNotThrow (
                () => new Function (normalCode, normalName, normalParameters, normalBody));
        }

        [Test]
        public void GuardedFunction ()
        {
            var emptyCode  = "";
            var weirdCode1 = "f a b = c";
            var weirdCode2 = "f a b | x - c";
            var normalCode = "f a b | x = c | otherwise = d";

            var normalName = "f";
            var weirdName  = "g";

            var normalParameters = new List <string> {"a", "b"};
            var weirdParameters  = new List <string> {"g"};

            var normalGuards = new List <GuardedFunction.Guard>
            {
                new GuardedFunction.Guard (new Atomic ("x", "x"), new Atomic ("c", "c")),
                new GuardedFunction.Guard (null, new Atomic ("d", "d"))
            };
            var weirdGuards1 = new List <GuardedFunction.Guard>
            {
                new GuardedFunction.Guard (new Atomic ("y", "y"), new Atomic ("c", "c"))
            };
            var weirdGuards2 = new List <GuardedFunction.Guard>
            {
                new GuardedFunction.Guard (new Atomic ("x", "x"), new Atomic ("z", "z"))
            };

            Assert.Throws <InvalidCodeException> (
                () => new GuardedFunction (emptyCode, normalName, normalParameters, normalGuards));
            Assert.Throws <InvalidCodeException> (
                () => new GuardedFunction (weirdCode1, normalName, normalParameters, normalGuards));
            Assert.Throws <InvalidCodeException> (
                () => new GuardedFunction (weirdCode2, normalName, normalParameters, normalGuards));
            Assert.Throws <InvalidCodeException> (
                () => new GuardedFunction (normalCode, weirdName, normalParameters, normalGuards));
            Assert.Throws <InvalidCodeException> (
                () => new GuardedFunction (normalCode, normalName, weirdParameters, normalGuards));
            Assert.Throws <InvalidCodeException> (
                () => new GuardedFunction (normalCode, normalName, normalParameters, weirdGuards1));
            Assert.Throws <InvalidCodeException> (
                () => new GuardedFunction (normalCode, normalName, normalParameters, weirdGuards2));
            Assert.DoesNotThrow (
                () => new GuardedFunction (normalCode, normalName, normalParameters, normalGuards));
        }

        [Test]
        public void If ()
        {
            var emptyCode  = "";
            var weirdCode1 = "i x then y else z";
            var weirdCode2 = "if x ten y else z";
            var weirdCode3 = "if x then y ese z";
            var normalCode = "if x then y else z";

            var normalPredicate = new Atomic ("x", "x");
            var weirdPredicate  = new Atomic ("a", "a");

            var normalThen = new Atomic ("y", "y");
            var weirdThen  = new Atomic ("a", "a");

            var normalElse = new Atomic ("z", "z");
            var weirdElse  = new Atomic ("a", "a");

            Assert.Throws <InvalidCodeException> (
                () => new If (emptyCode, normalPredicate, normalThen, normalElse));
            Assert.Throws <InvalidCodeException> (
                () => new If (weirdCode1, normalPredicate, normalThen, normalElse));
            Assert.Throws <InvalidCodeException> (
                () => new If (weirdCode2, normalPredicate, normalThen, normalElse));
            Assert.Throws <InvalidCodeException> (
                () => new If (weirdCode3, normalPredicate, normalThen, normalElse));
            Assert.Throws <InvalidCodeException> (
                () => new If (normalCode, weirdPredicate, normalThen, normalElse));
            Assert.Throws <InvalidCodeException> (
                () => new If (normalCode, normalPredicate, weirdThen, normalElse));
            Assert.Throws <InvalidCodeException> (
                () => new If (normalCode, normalPredicate, normalThen, weirdElse));
            Assert.DoesNotThrow (
                () => new If (normalCode, normalPredicate, normalThen, normalElse));
        }

        [Test]
        public void Parantheses ()
        {
            var emptyCode  = "";
            var weirdCode1 = "(f a b";
            var weirdCode2 = "f a b)";
            var normalCode = "(f a b)";

            var normalStatement = new Call ("f a b",
                                            "f",
                                            new List <Statement>
                                            {
                                                new Atomic ("a", "a"),
                                                new Atomic ("b", "b")
                                            });
            var weirdStatement = new Atomic ("g", "g");

            Assert.Throws <InvalidCodeException> (() => new Parantheses (emptyCode, normalStatement));
            Assert.Throws <InvalidCodeException> (() => new Parantheses (weirdCode1, normalStatement));
            Assert.Throws <InvalidCodeException> (() => new Parantheses (weirdCode2, normalStatement));
            Assert.Throws <InvalidCodeException> (() => new Parantheses (normalCode, weirdStatement));
            Assert.DoesNotThrow (() => new Parantheses (normalCode, normalStatement));
        }

        [Test]
        public void Tuple ()
        {
            var emptyCode  = "";
            var weirdCode1 = "x,y,z)";
            var weirdCode2 = "(x,y,z";
            var normalCode = "(x,y,z)";

            var normalElements = new List <Statement>
            {
                new Atomic ("x", "x"),
                new Atomic ("y", "y"),
                new Atomic ("z", "z")
            };
            var weirdElements = new List <Statement>
            {
                new Atomic ("a", "a")
            };

            Assert.Throws <InvalidCodeException> (() => new Tuple (emptyCode, normalElements));
            Assert.Throws <InvalidCodeException> (() => new Tuple (weirdCode1, normalElements));
            Assert.Throws <InvalidCodeException> (() => new Tuple (weirdCode2, normalElements));
            Assert.Throws <InvalidCodeException> (() => new Tuple (normalCode, weirdElements));
            Assert.DoesNotThrow (() => new Tuple (normalCode, normalElements));
        }

        [Test]
        public void GenericType ()
        {
            var emptyName  = "";
            var normalName = "Int";

            var normalType = Type.Int;
            var weirdType  = Type.Integer;

            Assert.Throws <InvalidCodeException> (() => new TypeDeclaration.GenericType (emptyName, normalType));
            Assert.Throws <InvalidCodeException> (() => new TypeDeclaration.GenericType (normalName, weirdType));
            Assert.DoesNotThrow (() => new TypeDeclaration.GenericType (normalName, normalType));
        }

        [Test]
        public void TypeDeclaration ()
        {
            var emptyCode   = "";
            var weirdCode   = "f : a";
            var normalCode1 = "f :: a";
            var normalCode2 = "f :: (Eq a, Ord b) => a -> b -> Int";

            var normalTypeName = "f";
            var weirdTypeName  = "g";

            var normalRestrictions1 = new HashSet <TypeRestriction> ();
            var normalRestrictions2 = new HashSet <TypeRestriction>
            {
                new TypeRestriction ("a", TypeClass.Eq),
                new TypeRestriction ("b", TypeClass.Ord)
            };
            var weirdRestrictions21 = new HashSet <TypeRestriction>
            {
                new TypeRestriction ("c", TypeClass.Eq)
            };
            var weirdRestrictions22 = new HashSet <TypeRestriction>
            {
                new TypeRestriction ("a", TypeClass.Bounded)
            };

            var normalParameterTypes1 = new List <TypeDeclaration.GenericType> ();
            var normalParameterTypes2 = new List <TypeDeclaration.GenericType>
            {
                new TypeDeclaration.GenericType ("a"),
                new TypeDeclaration.GenericType ("b"),
            };
            var weirdParameterTypes21 = new List <TypeDeclaration.GenericType>
            {
                new TypeDeclaration.GenericType ("c")
            };
            var weirdParameterTypes22 = new List <TypeDeclaration.GenericType>
            {
                new TypeDeclaration.GenericType (Type.Bool)
            };

            var normalReturnType1 = new TypeDeclaration.GenericType ("a");
            var normalReturnType2 = new TypeDeclaration.GenericType (Type.Int);
            var weirdReturnType21 = new TypeDeclaration.GenericType ("c");
            var weirdReturnType22 = new TypeDeclaration.GenericType (Type.Bool);

            Assert.Throws <InvalidCodeException> (
                () => new TypeDeclaration (emptyCode, normalTypeName, normalRestrictions1, normalParameterTypes1, normalReturnType1));
            Assert.Throws <InvalidCodeException> (
                () => new TypeDeclaration (weirdCode, normalTypeName, normalRestrictions1, normalParameterTypes1, normalReturnType1));
            Assert.Throws <InvalidCodeException> (
                () => new TypeDeclaration (normalCode2, weirdTypeName, normalRestrictions2, normalParameterTypes2, normalReturnType2));
            Assert.Throws <InvalidCodeException> (
                () => new TypeDeclaration (normalCode2, normalTypeName, weirdRestrictions21, normalParameterTypes2, normalReturnType2));
            Assert.Throws <InvalidCodeException> (
                () => new TypeDeclaration (normalCode2, normalTypeName, weirdRestrictions22, normalParameterTypes2, normalReturnType2));
            Assert.Throws <InvalidCodeException> (
                () => new TypeDeclaration (normalCode2, normalTypeName, normalRestrictions2, weirdParameterTypes21, normalReturnType2));
            Assert.Throws <InvalidCodeException> (
                () => new TypeDeclaration (normalCode2, normalTypeName, normalRestrictions2, weirdParameterTypes22, normalReturnType2));
            Assert.Throws <InvalidCodeException> (
                () => new TypeDeclaration (normalCode2, normalTypeName, normalRestrictions2, normalParameterTypes2, weirdReturnType21));
            Assert.Throws <InvalidCodeException> (
                () => new TypeDeclaration (normalCode2, normalTypeName, normalRestrictions2, normalParameterTypes2, weirdReturnType22));
            Assert.DoesNotThrow (
                () => new TypeDeclaration (normalCode1, normalTypeName, normalRestrictions1, normalParameterTypes1, normalReturnType1));
            Assert.DoesNotThrow (
                () => new TypeDeclaration (normalCode2, normalTypeName, normalRestrictions2, normalParameterTypes2, normalReturnType2));
        }
    }
}