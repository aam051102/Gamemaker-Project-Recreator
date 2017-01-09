using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;

namespace Gamemaker_Recompiler_Visual
{
    public partial class Form : System.Windows.Forms.Form
    {
        public static bool running = false;
        public static List<string> log_text = new List<string> { "" };

        public Form()
        {
            InitializeComponent();
            System.Timers.Timer timer = new System.Timers.Timer(1000);
            timer.Elapsed += async (sender, e) => await Update_Log();
            timer.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private async Task<string> Update_Log()
        {
            rtb_Output.Clear();
            for (var i = 0; i < log_text.Count; i++)
            {
                rtb_Output.AppendText(log_text[i]);
            }

            return "";
        }

        // SPRITES
        private void btn_Sprites_Click(object sender, EventArgs e)
        {
            if (running == false)
            {
                running = true;
                var loc_a = System.Reflection.Assembly.GetEntryAssembly().Location.LastIndexOf("\\") + "\\".Length;
                var loc_b = System.Reflection.Assembly.GetEntryAssembly().Location.Length - loc_a;
                Sprites.Convert_Sprites_From_Path(System.Reflection.Assembly.GetEntryAssembly().Location.Remove(loc_a) + "sprites\\");
            }
        }

        private void btn_Sprites_MouseUp(object sender, MouseEventArgs e)
        {
            btn_Sprites.ForeColor = Color.White;
        }

        private void btn_Sprites_MouseDown(object sender, MouseEventArgs e)
        {
            btn_Sprites.ForeColor = Color.Green;
        }

        private void btn_Sprites_MouseMove(object sender, MouseEventArgs e)
        {
            btn_Sprites.ForeColor = Color.Yellow;
        }

        private void btn_Sprites_MouseLeave(object sender, EventArgs e)
        {
            btn_Sprites.ForeColor = Color.White;
        }

        // ROOMS
        private void btn_Rooms_Click(object sender, EventArgs e)
        {
            if (running == false)
            {
                running = true;
                var loc_a = System.Reflection.Assembly.GetEntryAssembly().Location.LastIndexOf("\\") + "\\".Length;
                var loc_b = System.Reflection.Assembly.GetEntryAssembly().Location.Length - loc_a;
                Rooms.Convert_Rooms_From_Path(System.Reflection.Assembly.GetEntryAssembly().Location.Remove(loc_a) + "rooms\\");
            }
        }

        private void btn_Rooms_MouseUp(object sender, MouseEventArgs e)
        {
            btn_Rooms.ForeColor = Color.White;
        }

        private void btn_Rooms_MouseDown(object sender, MouseEventArgs e)
        {
            btn_Rooms.ForeColor = Color.Green;
        }

        private void btn_Rooms_MouseMove(object sender, MouseEventArgs e)
        {
            btn_Rooms.ForeColor = Color.Yellow;
        }

        private void btn_Rooms_MouseLeave(object sender, EventArgs e)
        {
            btn_Rooms.ForeColor = Color.White;
        }

        // BACKGROUNDS
        private void btn_Backgrounds_Click(object sender, EventArgs e)
        {
            if (running == false)
            {
                running = true;
                var loc_a = System.Reflection.Assembly.GetEntryAssembly().Location.LastIndexOf("\\") + "\\".Length;
                var loc_b = System.Reflection.Assembly.GetEntryAssembly().Location.Length - loc_a;
                Backgrounds.Convert_Backgrounds_From_Path(System.Reflection.Assembly.GetEntryAssembly().Location.Remove(loc_a) + "backgrounds\\");
            }
        }

        private void btn_Backgrounds_MouseUp(object sender, MouseEventArgs e)
        {
            btn_Backgrounds.ForeColor = Color.White;
        }

        private void btn_Backgrounds_MouseDown(object sender, MouseEventArgs e)
        {
            btn_Backgrounds.ForeColor = Color.Green;
        }

        private void btn_Backgrounds_MouseMove(object sender, MouseEventArgs e)
        {
            btn_Backgrounds.ForeColor = Color.Yellow;
        }

        private void btn_Backgrounds_MouseLeave(object sender, EventArgs e)
        {
            btn_Backgrounds.ForeColor = Color.White;
        }

        // OBJECTS
        private void btn_Objects_Click(object sender, EventArgs e)
        {
            if (running == false)
            {
                running = true;
                var loc_a = System.Reflection.Assembly.GetEntryAssembly().Location.LastIndexOf("\\") + "\\".Length;
                var loc_b = System.Reflection.Assembly.GetEntryAssembly().Location.Length - loc_a;
                Objects.Convert_Objects_From_Path(System.Reflection.Assembly.GetEntryAssembly().Location.Remove(loc_a) + "objects\\");
            }
        }

        private void btn_Objects_MouseUp(object sender, MouseEventArgs e)
        {
            btn_Objects.ForeColor = Color.White;
        }

        private void btn_Objects_MouseDown(object sender, MouseEventArgs e)
        {
            btn_Objects.ForeColor = Color.Green;
        }

        private void btn_Objects_MouseMove(object sender, MouseEventArgs e)
        {
            btn_Objects.ForeColor = Color.Yellow;
        }

        private void btn_Objects_MouseLeave(object sender, EventArgs e)
        {
            btn_Objects.ForeColor = Color.White;
        }

        // SCRIPTS
        private void btn_Scripts_Click(object sender, EventArgs e)
        {
            if (running == false)
            {
                running = true;
                var loc_a = System.Reflection.Assembly.GetEntryAssembly().Location.LastIndexOf("\\") + "\\".Length;
                var loc_b = System.Reflection.Assembly.GetEntryAssembly().Location.Length - loc_a;
                Scripts.Convert_Scripts_From_Path(System.Reflection.Assembly.GetEntryAssembly().Location.Remove(loc_a) + "scripts\\");
            }
        }

        private void btn_Scripts_MouseUp(object sender, MouseEventArgs e)
        {
            btn_Scripts.ForeColor = Color.White;
        }

        private void btn_Scripts_MouseDown(object sender, MouseEventArgs e)
        {
            btn_Scripts.ForeColor = Color.Green;
        }

        private void btn_Scripts_MouseMove(object sender, MouseEventArgs e)
        {
            btn_Scripts.ForeColor = Color.Yellow;
        }

        private void btn_Scripts_MouseLeave(object sender, EventArgs e)
        {
            btn_Scripts.ForeColor = Color.White;
        }

        // SOUNDS
        private void btn_Sounds_Click(object sender, EventArgs e)
        {
            if (running == false)
            {
                running = true;
                var loc_a = System.Reflection.Assembly.GetEntryAssembly().Location.LastIndexOf("\\") + "\\".Length;
                var loc_b = System.Reflection.Assembly.GetEntryAssembly().Location.Length - loc_a;
                Sounds.Convert_Sounds_From_Path(System.Reflection.Assembly.GetEntryAssembly().Location.Remove(loc_a) + "sounds\\");
            }
        }

        private void btn_Sounds_MouseUp(object sender, MouseEventArgs e)
        {
            btn_Sounds.ForeColor = Color.White;
        }

        private void btn_Sounds_MouseDown(object sender, MouseEventArgs e)
        {
            btn_Sounds.ForeColor = Color.Green;
        }

        private void btn_Sounds_MouseMove(object sender, MouseEventArgs e)
        {
            btn_Sounds.ForeColor = Color.Yellow;
        }

        private void btn_Sounds_MouseLeave(object sender, EventArgs e)
        {
            btn_Sounds.ForeColor = Color.White;
        }

        // FONTS
        private void btn_Fonts_Click(object sender, EventArgs e)
        {
            if (running == false)
            {
                running = true;
                var loc_a = System.Reflection.Assembly.GetEntryAssembly().Location.LastIndexOf("\\") + "\\".Length;
                var loc_b = System.Reflection.Assembly.GetEntryAssembly().Location.Length - loc_a;
                Fonts.Convert_Fonts_From_Path(System.Reflection.Assembly.GetEntryAssembly().Location.Remove(loc_a) + "fonts\\");
            }
        }

        private void btn_Fonts_MouseUp(object sender, MouseEventArgs e)
        {
            btn_Fonts.ForeColor = Color.White;
        }

        private void btn_Fonts_MouseDown(object sender, MouseEventArgs e)
        {
            btn_Fonts.ForeColor = Color.Green;
        }

        private void btn_Fonts_MouseMove(object sender, MouseEventArgs e)
        {
            btn_Fonts.ForeColor = Color.Yellow;
        }

        private void btn_Fonts_MouseLeave(object sender, EventArgs e)
        {
            btn_Fonts.ForeColor = Color.White;
        }

        // IMAGES
        private void btn_Images_MouseClick(object sender, MouseEventArgs e)
        {
            if (running == false)
            {
                running = true;
                var loc_a = System.Reflection.Assembly.GetEntryAssembly().Location.LastIndexOf("\\") + "\\".Length;
                var loc_b = System.Reflection.Assembly.GetEntryAssembly().Location.Length - loc_a;
                Sprites.Create_Sprites(System.Reflection.Assembly.GetEntryAssembly().Location.Remove(loc_a) + @"sprites\");
            }
        }

        private void btn_Images_MouseUp(object sender, MouseEventArgs e)
        {
            btn_Images.ForeColor = Color.White;
        }

        private void btn_Images_MouseDown(object sender, MouseEventArgs e)
        {
            btn_Images.ForeColor = Color.Green;
        }

        private void btn_Images_MouseMove(object sender, MouseEventArgs e)
        {
            btn_Images.ForeColor = Color.Yellow;
        }

        private void btn_Images_MouseLeave(object sender, EventArgs e)
        {
            btn_Images.ForeColor = Color.White;
        }

        // IMAGES 2
        private void btn_Images2_MouseClick(object sender, MouseEventArgs e)
        {
            if (running == false)
            {
                running = true;
                var loc_a = System.Reflection.Assembly.GetEntryAssembly().Location.LastIndexOf("\\") + "\\".Length;
                var loc_b = System.Reflection.Assembly.GetEntryAssembly().Location.Length - loc_a;
                Backgrounds.Create_Backgrounds(System.Reflection.Assembly.GetEntryAssembly().Location.Remove(loc_a) + @"backgrounds\");
            }
        }

        private void btn_Images2_MouseUp(object sender, MouseEventArgs e)
        {
            btn_Images2.ForeColor = Color.White;
        }

        private void btn_Images2_MouseDown(object sender, MouseEventArgs e)
        {
            btn_Images2.ForeColor = Color.Green;
        }

        private void btn_Images2_MouseMove(object sender, MouseEventArgs e)
        {
            btn_Images2.ForeColor = Color.Yellow;
        }

        private void btn_Images2_MouseLeave(object sender, EventArgs e)
        {
            btn_Images2.ForeColor = Color.White;
        }

        // IMAGES 3
        private void btn_Images3_MouseClick(object sender, MouseEventArgs e)
        {
            if (running == false)
            {
                running = true;
                var loc_a = System.Reflection.Assembly.GetEntryAssembly().Location.LastIndexOf("\\") + "\\".Length;
                var loc_b = System.Reflection.Assembly.GetEntryAssembly().Location.Length - loc_a;
                Fonts.Create_Fonts(System.Reflection.Assembly.GetEntryAssembly().Location.Remove(loc_a) + @"fonts\");
            }
        }

        private void btn_Images3_MouseUp(object sender, MouseEventArgs e)
        {
            btn_Images3.ForeColor = Color.White;
        }

        private void btn_Images3_MouseDown(object sender, MouseEventArgs e)
        {
            btn_Images3.ForeColor = Color.Green;
        }

        private void btn_Images3_MouseMove(object sender, MouseEventArgs e)
        {
            btn_Images3.ForeColor = Color.Yellow;
        }

        private void btn_Images3_MouseLeave(object sender, EventArgs e)
        {
            btn_Images3.ForeColor = Color.White;
        }
    }
}
