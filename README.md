# CSV for .NET
A small utility that will help you read your CSV files and parse into an `IEnumerable` of your POCO as you define it. This small and simple, yet feature rich and stable.

You can map and customise your **data type** and headers using `Attribute` and easily convert to required data type on serialisation and de-serialisation.

The following features are implemented:

 - Deserialise CSV to POCO
 - Type conversation on serialisation and de-serialisation.
 - Different input and output methods.

##Usage
The usage complexity for Csv.NET is defined as **beginner** and very simple to use. Please follow the below code for serialisation/de-serialisation:

###Deserialize CSV from file
    List<Person> persons = CsvNet.Deserialize<Person>(@"/path/to/csv/file");
  
###Deserialize CSV from string
    List<Person> persons = CsvNet.Deserialize<Person>(csvString);
    
###Serialize CSV to file
    List<Person> persons = ...
    ...
    ...
    CsvNet.Serialize(persons, @"/path/to/new/csv/file");

##Contributing

Pull requests are always welcome. For bugs and feature requests, please create an issue.

##Authors
**Abdul Munim**

- [github/munim](https://github.com/munim)
- [facebook/munim](https://fb.com/munim)
- [twitter/munim](https://twitter.com/munim)

##License

You are free to use this code anywhere from your personal and commercial projects but you are requested to keep the code and library intact while distribution.

Copyright © 2015, Abdul Munim and released under MIT license.

