namespace catalogServiceAPI.Models;

class Products {
public int ProductID {get; set;}
public string ProductName {get; set;}
public string ProductDescription {get; set;}
public int ProductStartPrice{get; set;}
public int ProductPrice{get; set;}
public int CurrentBid {get; set;}
public DateTime ProductStartDate {get; set;}
public DateTime ProductEndDate {get; set;}

}