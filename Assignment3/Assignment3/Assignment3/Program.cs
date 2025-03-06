using System;

namespace Assignment3
{
    public abstract class Shape
    {
        public abstract double GetArea();
        public abstract bool Isvaild();
    }
    //长方形
    public class Rectangle : Shape
    {
        //长与宽
        double length;
        double width;

        //构造函数
        public Rectangle(double length, double width)
        {
            this.length = length;
            this.width = width;
        }

        //计算面积
        public override double GetArea()
        {
            return Isvaild()? length * width:0;
        }

        //判断是否合法
        public override bool Isvaild()
        {
            return length > 0 && width > 0;
        }
    }
    //正方形
    public class Square : Shape
    {
        //边长
        double side;

        //构造函数
        public Square(double side)
        {
            this.side = side;
        }

        //计算面积
        public override double GetArea()
        {
            return Isvaild()? side * side:0;
        }

        //判断是否合法
        public override bool Isvaild()
        {
            return side > 0;
        }
    }
    //三角形
    public class Triangle : Shape
    {
        //三边
        double side1;
        double side2;
        double side3;

        //构造函数
        public Triangle(double side1, double side2, double side3)
        {
            this.side1 = side1;
            this.side2 = side2;
            this.side3 = side3;
        }

        //计算面积
        public override double GetArea()
        {
            double p = (side1 + side2 + side3) / 2;
            return Isvaild()? Math.Sqrt(p * (p - side1) * (p - side2) * (p - side3)):0;
        }

        //判断是否合法
        public override bool Isvaild()
        {
            return side1 > 0 && side2 > 0 && side3 > 0 && side1 + side2 > side3 && side1 + side3 > side2 && side2 + side3 > side1;
        }
    }


    //工厂类
    public class ShapeFactory
    {
        public static Shape CreateShape()
        {
            Random ran = new Random();
            int type = ran.Next(0, 2);
            switch (type)
            {
                case 0:
                    return new Rectangle(ran.NextDouble() * 10, ran.NextDouble() * 10);
                case 1:
                    return new Square(ran.NextDouble() * 10);
                case 2:
                    return new Triangle(ran.NextDouble() * 10, ran.NextDouble() * 10, ran.NextDouble() * 10);
                default:
                    throw new ArgumentException("错误");
            }
        }
    }

    public class Program
    {
        public static void Main()
        {
            // 创建10个随机形状
            List<Shape> shapes = new List<Shape>();
            for (int i = 0; i < 10; i++)
            {
                shapes.Add(ShapeFactory.CreateShape());
            }

            // 计算总面积
            double totalArea = shapes.Sum(s => s.GetArea());
            Console.WriteLine($"总面积为：{totalArea:F2}");

        }
    }
}