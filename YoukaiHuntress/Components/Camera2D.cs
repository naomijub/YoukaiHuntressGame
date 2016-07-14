using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoukaiHuntress.Components
{
    public static class Camera2D
    {
        public static Vector2 m_pos;
        public static float m_rot;
        public static float m_zoom;
        public static Vector2 m_origin;

        public static void SetViewport(Viewport viewport) {
            m_pos = new Vector2(viewport.Width * 0.5f, viewport.Height * 0.5f);
            m_rot = 0;
            m_zoom = 1;
            m_origin = new Vector2(viewport.Width * 0.5f, viewport.Height * 0.5f);
        }

        public static void SetPos(Vector2 pos) {
            float posX = 0.0f, posY = 320.0f;
            if (pos.X > 480)
            {
                if (pos.X < 1440)
                {
                    posX = pos.X;
                    // m_pos = pos;
                }
                else {
                    posX = 1440.0f;
                    //m_pos = new Vector2(1440, pos.Y);
                }
            }
            else {
                posX = 480.0f;
                //m_pos = new Vector2(480, pos.Y);
            }

            if (pos.Y > 320)
            {
                posY = 320.0f;
            }
            else {
                posY = pos.Y;
            }

            m_pos = new Vector2(posX, posY);
            
        }

        public static Matrix GetViewMatrix()
        {
            return
                Matrix.CreateTranslation(new Vector3(-m_pos, 0.0f)) *
                Matrix.CreateRotationZ(m_rot) *
                Matrix.CreateScale(m_zoom, m_zoom, 1) *
                Matrix.CreateTranslation(new Vector3(m_origin, 0.0f));
        }
    }
}
