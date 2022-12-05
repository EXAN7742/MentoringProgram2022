// See https://aka.ms/new-console-template for more information
using Composite;
using Composite.Task1;

Console.WriteLine("Task 1");
Form group1 = new Form("MyForm1");
group1.Add(new LabelText("First name"));
group1.Add(new InputText("FirstName", "John"));

Form group2 = new Form("Nested form");
group2.Add(new LabelText("Some info"));

group1.Add(group2);

Console.WriteLine(group1.ConvertToString());
