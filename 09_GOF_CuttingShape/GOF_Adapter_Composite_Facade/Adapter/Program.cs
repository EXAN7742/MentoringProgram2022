// See https://aka.ms/new-console-template for more information
using Adapter;

MyElements<string> myElements = new MyElements<string>() { MyItems = new List<string>() {"myItem1", "myItem2" } };
MyAdapter<string> adapter = new MyAdapter<string>(myElements);
Printer printer = new Printer();
printer.Print(adapter);
