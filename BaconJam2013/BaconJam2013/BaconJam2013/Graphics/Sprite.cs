﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace BaconJam2013
{
    public class Sprite
    {

        public Texture2D
            Texture;

        public Rectangle
            Source;

        public Sprite(Texture2D texture, Rectangle source)
        {
            Texture = texture;
            Source = source;
        }

    }
}
