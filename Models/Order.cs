namespace CarBuilder.Models;

public class Order
{
    public int Id { get; set; }
    public DateTime TimeStamp { get; set; }
    public int WheelId { get; set; }
    public Wheel Wheel { get; set; }
    public int TechnologyId { get; set; }
    public Technology Technology { get; set; }
    public int PaintId { get; set; }
    public PaintColor Paint { get; set; }
    public int InteriorId { get; set; }
    public Interior Interior { get; set; }
    public decimal TotalCost
    {
        get
        {
            return Paint.Price + Interior.Price + Wheel.Price + Technology.Price;
        }
    }
}
