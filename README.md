# TableParser
For a set of elements of type T (IEnumeable <T>), where the state of objects of type T is described by a set of public properties that have only date types, numeric, string and character, implement the ability to write in a table form to a text stream.
The number of columns in the table is determined by the number of public properties of each record. The width of each column is determined by the maximum number of characters required to stream a single property. At the same time, numbers and dates must be right-aligned, strings and characters left-aligned. For example,
Check the work of the developed functionality using the example of any class that has the above set of properties, using the output to a text file and the console.
