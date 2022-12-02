namespace ToyRobotCode.Services.Models
{
    public sealed class TableDimensions
    {
        public int Length { get; set; }
        public int Width { get; set; }
        public TableDimensions(int length, int width)
        {
            Length = length;
            Width = width;
        }

    }
}
