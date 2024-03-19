using System;
class car
{
    private string brand;
    private string type;
    private string color;
    private int year;

    public car(string brand, string type, string color, int year)
    {
        this.brand = brand;
        this.type = type;
        this.color = color;
        this.year = year;
    }
    public string Brand
    {
        get {return brand;}
        set {brand = value;}
    }
    public string Type
    {
        get { return type; }
        set { type = value; }
    }

    public string Color
    {
        get { return color; }
        set { color = value; }
    }

    public int Year
    {
        get { return year; }
        set { year = value; }
    }
}



