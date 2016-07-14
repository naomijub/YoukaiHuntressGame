using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace YoukaiHuntress.Components
{
    public class Triangle
    {
        public Vector2[] points { get; set; }
        public Vector2 normal { get; set; }

        public Triangle(Vector2[] points) {
            this.points = points;
            setNormal();
            //foreach (Vector2 point in points)
            //{
            //    Console.WriteLine(point.ToString());
            //}
        }

        private void setNormal()
        {
            float x = (points[1].Y - points[0].Y) / (points[1].X - points[0].X);
            normal = new Vector2(x, -1);
        }

        public bool isPointInsideTriangle(Vector2 point) {
            float triangleArea = TriangleArea(points[0], points[1], points[2]);
            float pointArea = TriangleArea(point, points[0], points[1]) + TriangleArea(point, points[0], points[2]) + TriangleArea(point, points[1], points[2]);
            Console.WriteLine("Point area: " + pointArea + " triangle area: " + triangleArea);

            if (pointArea > triangleArea) {
                return false;
            }
            return true;
        }

        public float TriangleArea(Vector2 p1, Vector2 p2, Vector2 p3) {
            float area = 0.0f;
            area = (p1.X - p3.X) * (p2.Y - p3.Y) - (p2.X - p3.X) * (p1.Y - p3.Y);
            return (area / 2.0f);
        }
    }
}
