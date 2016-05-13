# Sigma 

Sigma allows you define a predicate without using any construction of target language and then translate it to language you wish. Moreover, library allows you to define set of target languages with several lines of XML code.  


----------


# Usage examples
#### Define a target language

In example below SQL language is defined. 

```XML
 <LanguageDefinition Name="SQL">

    <MetadataDefinition OpenBracket="(" CloseBracket=")" Prioritizer="BracketPrioritizer" />

    <LogicalOperatorDefinitions>
      <LogicalOperatorDefinition Operator="{AND}" Implementation="{0} AND {1}" />
      <LogicalOperatorDefinition Operator="{OR}" Implementation="{0} OR {1}" />
      <LogicalOperatorDefinition Operator="{NOT}" Implementation="NOT {0}" />
    </LogicalOperatorDefinitions>

    <PredicateDefinitions>
      <PredicateDefinition Operator="{Equal}" Implementation="{0} = {1}" />
      <PredicateDefinition Operator="{NotEqual}" Implementation="{0} != {1}" />
      <!-- ... -->
      <PredicateDefinition Operator="{Between}" Implementation=" {0} BETWEEN {1} AND {2}" />
      <PredicateDefinition Operator="{HasSubstring}" Implementation="{0} like %{1}%" />
      <!-- ... -->
      <PredicateDefinition Operator="{In}" Implementation="{0} in {1}" />
      <!-- ... -->
    </PredicateDefinitions>
  </LanguageDefinition>

```

You can find  full example of SQL language definition in corresponding Unit Test.

#### Initialization of specification layer and language registration

> Keep in mind, that any application that uses Sigma depends on XSD file from Specification layer and XML file > defined by user (find examples in Unit Tests)


In example below, please find how to intialize Sigma and register language processor. 

```C#
	// Initialize specification by XML path
    Specification.Specification.Initialize(LanguageDefinitionsPath);

	// Register SQL Language
    FormulaProcessorRepository
    		.Instance
            .RegisterFormulaProcessor(new DefaultFormulaProcessor("SQL"));
```

So, Default Formula Processor is created and registered for SQL language. DefaultFormulaProcessor class aggregates main functionality of translator (processor), but, anyway, it's functionality is overridable, so that you can easily inherit and redefine it's behavior. 


#### Build a formula to process

Sigma provides multiple methods of building predicates. As we kept Sigma Core independent from language constructions, logical operators and predicates, you can define your own wrapper to build formulas, or just use shortcuts defined in Expressions layer. 

Here is sample usage of Sigma Expression shortcuts:

```C#
	// corresponding C# code
  	// a == b && (b == c || a == d)
   
  	var formula = new Formula
  	(
  		SigExp.And
  		(
  			SigExp.Equal(a, b),
  			SigExp.Or
  			(
  				SigExp.Not
  				(
  					SigExp.Equal(b, c)
  				),
  				SigExp.Equal(a, d)
  			)
  		)
	);
```

Code should prepare a definition of logical formula "a == b && (b == c || a == d)". 

#### Process formula to target language

To perform processing you need to request language processor first.

```C#
// request sql formula processor 
var sql = FormulaProcessorRepository
                .Instance
                .GetFormulaProcessor("SQL");
```


Then perform translation:
```C#
var resultSQL = sql.Process(formula);
```













