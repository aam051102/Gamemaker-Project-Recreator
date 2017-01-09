using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing;

namespace Gamemaker_Recompiler_Visual
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form());
        }
    }

    static class Rooms
    {
        public static void Convert_Rooms_From_Path(string path)
        {
            string[] rooms = Rooms.Get_Files(path);
            foreach (string room in rooms)
            {
                if (Path.GetExtension(Path.GetFileName(room)) == ".xml")
                {
                    Rooms.Convert_Room(path, Path.GetFileName(room));
                }
            }
            Form.running = false;
            Form.log_text.Add("Finished room conversion." + System.Environment.NewLine + rooms.Length + " files processed.");
        }

        public static void Convert_Room(string path, string file)
        {
            string text = System.IO.File.ReadAllText(path + file);

            // GENERAL INFORMATION
            // WIDTH
            int width_a = text.IndexOf("<width>") + "<width>".Length;
            int width_b = text.IndexOf("</width>");
            string width = text.Substring(width_a, width_b - width_a);
            System.Console.WriteLine("Width: {0}", width);

            // HEIGHT
            int height_a = text.IndexOf("<height>") + "<height>".Length;
            int height_b = text.IndexOf("</height>");
            string height = text.Substring(height_a, height_b - height_a);
            System.Console.WriteLine("Height: {0}", height);

            // NAME
            int name_a = text.IndexOf("<name>") + "<name>".Length;
            int name_b = text.IndexOf("</name>");
            string name = text.Substring(name_a, name_b - name_a);
            System.Console.WriteLine("Name: {0}", name);

            // SPEED
            int speed_a = text.IndexOf("<speed>") + "<speed>".Length;
            int speed_b = text.IndexOf("</speed>");
            string speed = text.Substring(speed_a, speed_b - speed_a);
            System.Console.WriteLine("Speed: {0}", speed);

            // OBJECTS
            // OBJECTS
            int objects_a = text.IndexOf("<Objects>") + "<Objects>".Length;
            int objects_b = text.IndexOf("</Objects>");
            string objects = text.Substring(objects_a, objects_b - objects_a);
            System.Console.WriteLine("Objects: {0}", objects);

            // OBJECT INDEXES
            int object_indexes_a = objects.LastIndexOf("<index>") + "<index>".Length;
            int object_indexes_b = objects.LastIndexOf("</index>");
            string object_indexes = objects.Substring(object_indexes_a, object_indexes_b - object_indexes_a);

            // INDEX
            int index_a = objects.IndexOf("<index>") + "<index>".Length;
            int index_b = objects.IndexOf("</index>");
            string index = objects.Substring(index_a, index_b - index_a);

            // OBJECT X
            int[] instance_x = new int[1000];
            int instance_x_a = 0;
            int instance_x_b = 0;

            // OBJECT Y
            int[] instance_y = new int[1000];
            int instance_y_a = 0;
            int instance_y_b = 0;

            // OBJECT NAME
            string[] instance_name = new string[1000];
            int instance_name_a = 0;
            int instance_name_b = 0;

            // OBJECT ID
            int[] instance_id = new int[1000];
            int instance_id_a = 0;
            int instance_id_b = 0;

            // OBJECT INDEX
            int[] instance_index = new int[1000];
            int instance_index_a = 0;
            int instance_index_b = 0;

            // OBJECT X SCALE
            double[] instance_xscale = new double[1000];
            int instance_xscale_a = 0;
            int instance_xscale_b = 0;

            // OBJECT Y SCALE
            double[] instance_yscale = new double[1000];
            int instance_yscale_a = 0;
            int instance_yscale_b = 0;

            // OBJECT ROTATION
            int[] instance_rot = new int[1000];
            int instance_rot_a = 0;
            int instance_rot_b = 0;

            // OBJECT CREATION CODE
            string[] instance_cc = new string[1000];
            int instance_cc_a = 0;
            int instance_cc_b = 0;

            //System.Console.WriteLine(object_indexes);
            //System.Console.Read();
            if (object_indexes != "-1")
            {
                //System.Console.WriteLine("Object Indexes: {0}", object_indexes);

                // BASIC OBJECT INFO
                string file_room_instances = objects;
                string[] file_room_instance = new string[1000];
                int file_room_instances_a = 0;
                int file_room_instances_b = 0;
                string txt = "";

                for (int n = 0; n < Convert.ToInt32(object_indexes) + 1; n++)
                {
                    // BASIC OBJECT INFO
                    file_room_instances_a = file_room_instances.IndexOf("<File.Room.Instance>") + "<File.Room.Instance>".Length;
                    file_room_instances_b = file_room_instances.IndexOf("</File.Room.Instance>");
                    txt = file_room_instances.Substring(file_room_instances_a, file_room_instances_b - file_room_instances_a);
                    file_room_instances = file_room_instances.Remove(0, file_room_instances.IndexOf("</File.Room.Instance>") + "</File.Room.Instance>".Length);

                    file_room_instance[n] = txt;

                    // OBJECT X
                    instance_x_a = file_room_instance[n].IndexOf("<X>") + "<X>".Length;
                    instance_x_b = file_room_instance[n].IndexOf("</X>");
                    instance_x[n] = Convert.ToInt32(file_room_instance[n].Substring(instance_x_a, instance_x_b - instance_x_a));

                    // OBJECT Y
                    instance_y_a = file_room_instance[n].IndexOf("<Y>") + "<Y>".Length;
                    instance_y_b = file_room_instance[n].IndexOf("</Y>");
                    instance_y[n] = Convert.ToInt32(file_room_instance[n].Substring(instance_y_a, instance_y_b - instance_y_a));

                    // OBJECT ID
                    instance_id_a = file_room_instance[n].IndexOf("<Id>") + "<Id>".Length;
                    instance_id_b = file_room_instance[n].IndexOf("</Id>");
                    instance_id[n] = Convert.ToInt32(file_room_instance[n].Substring(instance_id_a, instance_id_b - instance_id_a));

                    // OBJECT NAME
                    instance_name_a = file_room_instance[n].IndexOf("<Object_Name>") + "<Object_Name>".Length;
                    instance_name_b = file_room_instance[n].IndexOf("</Object_Name>");
                    instance_name[n] = file_room_instance[n].Substring(instance_name_a, instance_name_b - instance_name_a);

                    // OBJECT INDEX
                    instance_index_a = file_room_instance[n].IndexOf("<Object_Index>") + "<Object_Index>".Length;
                    instance_index_b = file_room_instance[n].IndexOf("</Object_Index>");
                    instance_index[n] = Convert.ToInt32(file_room_instance[n].Substring(instance_index_a, instance_index_b - instance_index_a));

                    // OBJECT X SCALE
                    instance_xscale_a = file_room_instance[n].IndexOf("<Scale_X>") + "<Scale_X>".Length;
                    instance_xscale_b = file_room_instance[n].IndexOf("</Scale_X>");
                    instance_xscale[n] = Convert.ToDouble(file_room_instance[n].Substring(instance_xscale_a, instance_xscale_b - instance_xscale_a));

                    // OBJECT Y SCALE
                    instance_yscale_a = file_room_instance[n].IndexOf("<Scale_Y>") + "<Scale_Y>".Length;
                    instance_yscale_b = file_room_instance[n].IndexOf("</Scale_Y>");
                    instance_yscale[n] = Convert.ToDouble(file_room_instance[n].Substring(instance_yscale_a, instance_yscale_b - instance_yscale_a));

                    // OBJECT ROTATION
                    instance_rot_a = file_room_instance[n].IndexOf("<Rotation>") + "<Rotation>".Length;
                    instance_rot_b = file_room_instance[n].IndexOf("</Rotation>");
                    instance_rot[n] = Convert.ToInt32(file_room_instance[n].Substring(instance_rot_a, instance_rot_b - instance_rot_a));

                    // OBJECT CREATION CODE
                    instance_cc_a = file_room_instance[n].IndexOf("<Room_Code>") + "<Room_Code>".Length;
                    instance_cc_b = file_room_instance[n].IndexOf("</Room_Code>");
                    //MessageBox.Show(Convert.ToString(instance_cc_b));
                    if (instance_cc_b != -1) {
                        instance_cc[n] = file_room_instance[n].Substring(instance_cc_a, instance_cc_b - instance_cc_a);
                    } else
                    {
                        instance_cc[n] = "";
                    }

                    //MessageBox.Show(file_room_instances);
                    //System.Console.WriteLine("file room instances: {0}", file_room_instances);
                    //System.Console.WriteLine(": {0}", file_room_instances.Substring(file_room_instances_a, file_room_instances_b - file_room_instances_a));
                    //System.Console.Read();
                }

                //System.Console.WriteLine("file room instances: {0}", file_room_instances);
                //System.Console.WriteLine(": {0}", file_room_instances.Substring(file_room_instances_a, file_room_instances_b - file_room_instances_a));
                //System.Console.Read();

                //System.Console.ReadLine();
            }

            //System.Console.ReadLine();

            // ROOM CREATION CODE 
            string creation_code = "";

            if (System.IO.File.Exists(path.Remove(path.Length - path.LastIndexOf("\\") - 1) + "code\\" + "gml_Room_" + name + ".js")) {
                creation_code = System.IO.File.ReadAllText(@path.Remove(path.Length - path.LastIndexOf("\\") - 1) + "code\\" + "gml_Room_" + name + ".js");
            } else
            {
                //MessageBox.Show(path.Remove(path.Length - path.LastIndexOf("\\") - 1) + "code\\" + "gml_Room_" + name + ".js");
            }

            // OTHER
            // PERSISTENT
            int persistent_a = text.IndexOf("<persistent>") + "<persistent>".Length;
            int persistent_b = text.IndexOf("</persistent>");
            string persistent = text.Substring(persistent_a, persistent_b - persistent_a);
            System.Console.WriteLine("Persistent: {0}", persistent);

            // COLOR
            int color_a = text.IndexOf("<color>") + "<color>".Length;
            int color_b = text.IndexOf("</color>");
            string color = text.Substring(color_a, color_b - color_a);
            System.Console.WriteLine("Color: {0}", color);

            // SHOW COLOR
            int show_color_a = text.IndexOf("<show_color>") + "<show_color>".Length;
            int show_color_b = text.IndexOf("</show_color>");
            string show_color = text.Substring(show_color_a, show_color_b - show_color_a);
            System.Console.WriteLine("Show Color: {0}", show_color);

            // ENABLE VIEWS
            int enable_views_a = text.IndexOf("<enable_views>") + "<enable_views>".Length;
            int enable_views_b = text.IndexOf("</enable_views>");
            string enable_views = text.Substring(enable_views_a, enable_views_b - enable_views_a);
            System.Console.WriteLine("Enable Views: {0}", enable_views);

            // VIEW CLEAR SCREEN
            int view_clear_screen_a = text.IndexOf("<view_clear_screen>") + "<view_clear_screen>".Length;
            int view_clear_screen_b = text.IndexOf("</view_clear_screen>");
            string view_clear_screen = text.Substring(view_clear_screen_a, view_clear_screen_b - view_clear_screen_a);
            System.Console.WriteLine("View Clear Screen: {0}", view_clear_screen);

            // CLEAR DISPLAY BUFFER
            int clear_display_buffer_a = text.IndexOf("<clear_display_buffer>") + "<clear_display_buffer>".Length;
            int clear_display_buffer_b = text.IndexOf("</clear_display_buffer>");
            string clear_display_buffer = text.Substring(clear_display_buffer_a, clear_display_buffer_b - clear_display_buffer_a);
            System.Console.WriteLine("Clear Display Buffer: {0}", clear_display_buffer);

            // VIEWS
            if (Convert.ToBoolean(enable_views) == true)
            {
                int view_area_a = text.IndexOf("<Views>") + "<Views>".Length;
                int view_area_b = text.IndexOf("</Views>");
                string view_area = text.Substring(view_area_a, view_area_b - view_area_a);

                int view_index_area_a = view_area.LastIndexOf("<ViewIndex>") + "<ViewIndex>".Length;
                int view_index_area_b = view_area.LastIndexOf("</ViewIndex>");
                string view_index_area = view_area.Substring(view_index_area_a, view_index_area_b - view_index_area_a);

                List<string> view = new List<string> { };
                List<bool> view_visible = new List<bool> { };
                List<int> view_width = new List<int> { };
                List<int> view_height = new List<int> { };

                // PORT
                List<int> view_port_x = new List<int> { };
                List<int> view_port_y = new List<int> { };
                List<int> view_port_width = new List<int> { };
                List<int> view_port_height = new List<int> { };

                // BORDER
                List<int> view_border_x = new List<int> { };
                List<int> view_border_y = new List<int> { };

                // SPEED
                List<int> view_speed_x = new List<int> { };
                List<int> view_speed_y = new List<int> { };

                // VIEW INDEX
                List<int> view_index = new List<int> { };

                for (var n = 0; n < Convert.ToUInt32(view_index_area) + 1; n++)
                {
                    int view_a = view_area.IndexOf("<File.Room.View>") + "<File.Room.View>".Length;
                    int view_b = view_area.IndexOf("</File.Room.View>");
                    view.Add(view_area.Substring(view_area_a, view_area_b - view_area_a));


                }
            }

            // REWRITE
            string rewrite = "";
            rewrite += "<!--This Document is generated by GameMaker, if you edit it by hand then you do so at your own risk!-->@";
            rewrite += "<room>@";

            rewrite += "    <caption></caption>@";
            rewrite += "    <width>" + width + "</width>@";
            rewrite += "    <height>" + height + "</height>@";
            rewrite += "    <vsnap>" + "1" + "</vsnap>@";
            rewrite += "    <hsnap>" + "1" + "</hsnap>@";
            rewrite += "    <isometric>" + "0" + "</isometric>@";
            rewrite += "    <speed>" + speed + "</speed>@";
            rewrite += "    <persistent>" + persistent + "</persistent>@";
            rewrite += "    <colour>" + color + "</colour>@";
            rewrite += "    <showcolour>" + show_color + "</showcolour>@";
            rewrite += "    <code>" + creation_code + "</code>@";
            rewrite += "    <enableViews>" + Convert.ToInt32(Convert.ToBoolean(enable_views)) + "</enableViews>@";
            rewrite += "    <clearViewBackground>" + Convert.ToInt32(Convert.ToBoolean(view_clear_screen)) + "</clearViewBackground>@";
            rewrite += "    <clearDisplayBuffer>" + Convert.ToInt32(Convert.ToBoolean(clear_display_buffer)) + "</clearDisplayBuffer>@";
            rewrite += "    <makerSettings>@";

            rewrite += "        <isSet>" + "0" + "</isSet>@";
            rewrite += "        <w>" + "0" + "</w>@";
            rewrite += "        <h>" + "0" + "</h>@";
            rewrite += "        <showGrid>" + "0" + "</showGrid>@";
            rewrite += "        <showObjects>" + "0" + "</showObjects>@";
            rewrite += "        <showTiles>" + "0" + "</showTiles>@";
            rewrite += "        <showBackgrounds>" + "0" + "</showBackgrounds>@";
            rewrite += "        <showForegrounds>" + "0" + "</showForegrounds>@";
            rewrite += "        <showViews>" + "0" + "</showViews>@";
            rewrite += "        <deleteUnderlyingObj>" + "0" + "</deleteUnderlyingObj>@";
            rewrite += "        <deleteUnderlyingTiles>" + "0" + "</deleteUnderlyingTiles>@";
            rewrite += "        <page>" + "0" + "</page>@";
            rewrite += "        <xoffset>" + "0" + "</xoffset>@";
            rewrite += "        <yoffset>" + "0" + "</yoffset>@";

            rewrite += "    </makerSettings>@";
            rewrite += "    <backgrounds>@";

            rewrite += "        <background visible=\"" + "0" + "\" foreground=\"" + "0" + "\" name=\"" + "" + "\" x=\"" + "0" + "\" y=\"" + "0" + "\" htiled=\"" + "-1" + "\" vtiled=\"" + "-1" + "\" hspeed=\"" + "0" + "\" vspeed=\"" + "0" + "\" stretch=\"" + "0" + "\"/>@";
            rewrite += "        <background visible=\"" + "0" + "\" foreground=\"" + "0" + "\" name=\"" + "" + "\" x=\"" + "0" + "\" y=\"" + "0" + "\" htiled=\"" + "-1" + "\" vtiled=\"" + "-1" + "\" hspeed=\"" + "0" + "\" vspeed=\"" + "0" + "\" stretch=\"" + "0" + "\"/>@";
            rewrite += "        <background visible=\"" + "0" + "\" foreground=\"" + "0" + "\" name=\"" + "" + "\" x=\"" + "0" + "\" y=\"" + "0" + "\" htiled=\"" + "-1" + "\" vtiled=\"" + "-1" + "\" hspeed=\"" + "0" + "\" vspeed=\"" + "0" + "\" stretch=\"" + "0" + "\"/>@";
            rewrite += "        <background visible=\"" + "0" + "\" foreground=\"" + "0" + "\" name=\"" + "" + "\" x=\"" + "0" + "\" y=\"" + "0" + "\" htiled=\"" + "-1" + "\" vtiled=\"" + "-1" + "\" hspeed=\"" + "0" + "\" vspeed=\"" + "0" + "\" stretch=\"" + "0" + "\"/>@";
            rewrite += "        <background visible=\"" + "0" + "\" foreground=\"" + "0" + "\" name=\"" + "" + "\" x=\"" + "0" + "\" y=\"" + "0" + "\" htiled=\"" + "-1" + "\" vtiled=\"" + "-1" + "\" hspeed=\"" + "0" + "\" vspeed=\"" + "0" + "\" stretch=\"" + "0" + "\"/>@";
            rewrite += "        <background visible=\"" + "0" + "\" foreground=\"" + "0" + "\" name=\"" + "" + "\" x=\"" + "0" + "\" y=\"" + "0" + "\" htiled=\"" + "-1" + "\" vtiled=\"" + "-1" + "\" hspeed=\"" + "0" + "\" vspeed=\"" + "0" + "\" stretch=\"" + "0" + "\"/>@";
            rewrite += "        <background visible=\"" + "0" + "\" foreground=\"" + "0" + "\" name=\"" + "" + "\" x=\"" + "0" + "\" y=\"" + "0" + "\" htiled=\"" + "-1" + "\" vtiled=\"" + "-1" + "\" hspeed=\"" + "0" + "\" vspeed=\"" + "0" + "\" stretch=\"" + "0" + "\"/>@";
            rewrite += "        <background visible=\"" + "0" + "\" foreground=\"" + "0" + "\" name=\"" + "" + "\" x=\"" + "0" + "\" y=\"" + "0" + "\" htiled=\"" + "-1" + "\" vtiled=\"" + "-1" + "\" hspeed=\"" + "0" + "\" vspeed=\"" + "0" + "\" stretch=\"" + "0" + "\"/>@";

            rewrite += "    </backgrounds>@";
            rewrite += "    <views>@";

            rewrite += "        <view visible=\"" + "0" + "\" objName=\"" + "&alt;undefined&gt;" + "\" xview=\"" + "0" + "\" yview=\"" + "0" + "\"wview=\"" + "1024" + "\" hview=\"" + "768" + "\" xport=\"" + "0" + "\" yport=\"" + "0" + "\" wport=\"" + "1024" + "\" hport=\"" + "768" + "\" hborder=\"" + "32" + "\" vborder=\"" + "32" + "\" hspeed=\"" + "-1" + "\" vspeed=\"" + "-1" + "\"/>";

            rewrite += "    </views>@";
            rewrite += "    <instances>@";

            if (object_indexes != "-1") {
                for (int i = 0; i < Convert.ToInt32(object_indexes) + 1; i++)
                {
                    if (instance_cc[i] != "") {
                        rewrite += "        <instance objName=\"" + instance_name[i] + "\" x=\"" + instance_x[i] + "\" y=\"" + instance_y[i] + "\" name=\"" + "inst_" + instance_id[i] + "\" locked=\"" + "0" + "\" code=\"" + instance_cc[i] + "\" scaleX=\"" + instance_xscale[i] + "\" scaleY=\"" + instance_yscale[i] + "\" colour=\"" + "4294967295" + "\" rotation=\"" + instance_rot[i] + "\"/>@";
                    } else {
                        rewrite += "        <instance objName=\"" + instance_name[i] + "\" x=\"" + instance_x[i] + "\" y=\"" + instance_y[i] + "\" name=\"" + "inst_" + instance_id[i] + "\" locked=\"" + "0" + "\" code=\"" + "" + "\" scaleX=\"" + instance_xscale[i] + "\" scaleY=\"" + instance_yscale[i] + "\" colour=\"" + "4294967295" + "\" rotation=\"" + instance_rot[i] + "\"/>@";
                    }
                }
            }

            rewrite += "    </instances>@";
            rewrite += "    <tiles/>@";
            rewrite += "    <PhysicsWorld>" + "0" + "</PhysicsWorld>@";
            rewrite += "    <PhysicsWorldTop>" + "0" + "</PhysicsWorldTop>@";
            rewrite += "    <PhysicsWorldLeft>" + "0" + "</PhysicsWorldLeft>@";
            rewrite += "    <PhysicsWorldRight>" + width + "</PhysicsWorldRight>@";
            rewrite += "    <PhysicsWorldBottom>" + height + "</PhysicsWorldBottom>@";
            rewrite += "    <PhysicsWorldGravityX>" + "0" + "</PhysicsWorldGravityX>@";
            rewrite += "    <PhysicsWorldGravityY>" + "0" + "</PhysicsWorldGravityY>@";
            rewrite += "    <PhysicsWorldPixToMeters>" + "0.0" + "</PhysicsWorldPixToMeters>@";
            rewrite += "</room>";

            rewrite = rewrite.Replace("@", "" + System.Environment.NewLine);

            System.IO.Directory.CreateDirectory(path + "converted\\");
            var File = System.IO.File.OpenWrite(path + "converted\\" + name + ".room.gmx");

            File.Close();

            System.IO.File.WriteAllText(path + "converted\\" + name + ".room.gmx", rewrite);

            Console.WriteLine(System.IO.File.ReadAllText(path + "converted\\" + name + ".room.gmx") +
                "\n has been output to file " + name + ".room.gmx" + ", path " + path + "converted");
        }

        public static string[] Get_Files(string path)
        {
            string[] files = Directory.GetFiles(path);

            return files;
        }
    }

    static class Objects
    {
        public static void Convert_Objects_From_Path(string path)
        {
            string[] objects = Scripts.Get_Files(path);
            foreach (string Object in objects)
            {
                if (Path.GetExtension(Path.GetFileName(Object)) == ".js")
                {
                    Objects.Convert_Object(path, Path.GetFileName(Object));
                }
            }
            Form.running = false;
            Form.log_text.Add("Finished object conversion." + System.Environment.NewLine + objects.Length + " files processed.");
        }

        public static void Convert_Object(string path, string file)
        {
            string text = System.IO.File.ReadAllText(path + file);

            //System.Console.WriteLine("Contents of WriteText.txt = {0}", text);
            if (text != "")
            {
                // GENERAL INFORMATION
                // NAME
                /*int name_a = text.IndexOf("Object:") + "Object:".Length;
                int name_b = text.IndexOf("//*");
                int name_c = text.IndexOf("builtin.name = \"") + "builtin.name = \"".Length;
                int name_d = text.IndexOf("\"");
                string name = text.Substring(name_a, name_b - name_a).Substring(name_c, name_d - name_c);*/
                string name = file.Remove(file.Length-3);

                // CODE
                //string[] code = new string[100];
                //code[0] = System.IO.File.ReadAllText(@path.Remove(path.Length - path.LastIndexOf("\\") - 1) + "code\\" + "gml_Object_" + name + ".js");

                // REWRITE
                string rewrite = "";
                rewrite += "<!--This Document is generated by GameMaker, if you edit it by hand then you do so at your own risk!-->@";
                rewrite += "<object>@";
                rewrite += "    <spriteName>" + name + "</spriteName>@";
                rewrite += "    <solid>" + "" + "</solid>@";
                rewrite += "    <visible>" + "" + "</visible>@";
                rewrite += "    <depth>" + "" + "</depth>@";
                rewrite += "    <persistent>" + "" + "</persistent>@";
                rewrite += "    <parentName>" + "" + "</parentName>@";
                rewrite += "    <maskName>" + "" + "</maskName>@";
                rewrite += "    <events>@";



                rewrite += "    </events>@";
                rewrite += "    <PhysicsObject>" + "" + "</PhysicsObject>@";
                rewrite += "    <PhysicsObjectSensor>" + "" + "</PhysicsObjectSensor>@";
                rewrite += "    <PhysicsObjectShape>" + "" + "</PhysicsObjectShape>@";
                rewrite += "    <PhysicsObjectDensity>" + "" + "</PhysicsObjectDensity>@";
                rewrite += "    <PhysicsObjectRestitution>" + "" + "</PhysicsObjectRestitution>@";
                rewrite += "    <PhysicsObjectGroup>" + "" + "</PhysicsObjectGroup>@";
                rewrite += "    <PhysicsObjectLinearDamping>" + "" + "</PhysicsObjectLinearDamping>@";
                rewrite += "    <PhysicsObjectAngularDamping>" + "" + "</PhysicsObjectAngularDamping>@";
                rewrite += "    <PhysicsObjectAwake>" + "" + "</PhysicsObjectAwake>@";
                rewrite += "    <PhysicsObjectKinematic>" + "" + "</PhysicsObjectKinematic>@";
                rewrite += "    <PhysicsShapePoints>" + "" + "</PhysicsShapePoints>@";
                rewrite += "</object>@";

                rewrite = rewrite.Replace("@", "" + System.Environment.NewLine);

                System.IO.Directory.CreateDirectory(path + "converted\\");
                var File = System.IO.File.OpenWrite(path + "converted\\" + name + ".object.gmx");

                File.Close();

                System.IO.File.WriteAllText(path + "converted\\" + name + ".object.gmx", rewrite);

                //Form.log_text.Add(System.IO.File.ReadAllText(path + "converted\\" + name + ".sprite.gmx") +
                //    "\n has been output to file " + name + ".sprite.gmx" + ", path " + path + "converted");
            }
        }

        public static string[] Get_Files(string path)
        {
            string[] files = Directory.GetFiles(path);

            return files;
        }
    }

    static class Scripts
    {
        public static void Convert_Scripts_From_Path(string path)
        {
            string[] scripts = Scripts.Get_Files(path);
            foreach (string script in scripts)
            {
                if (Path.GetExtension(Path.GetFileName(script)) == ".js")
                {
                    Scripts.Convert_Script(path, Path.GetFileName(script));
                }
            }
            Form.running = false;
            Form.log_text.Add("Finished script conversion." + System.Environment.NewLine + scripts.Length + " files processed.");
        }

        public static void Convert_Script(string path, string file)
        {
            string text = System.IO.File.ReadAllText(path + file);

            //System.Console.WriteLine("Contents of WriteText.txt = {0}", text);
            if (text != "")
            {
                // GENERAL INFORMATION
                // SCRIPT NAME
                int script_name_a = file.Length - 3;
                string script_name = text.Substring("// ScriptName: ".Length, script_name_a);

                // SCRIPT CODE
                string script_code = System.IO.File.ReadAllText(@path.Remove(path.Length-path.LastIndexOf("\\")-1) + "code\\" + "gml_Script_" + script_name + ".js");
                //MessageBox.Show(script_code);

                // REWRITE
                string rewrite = "";
                rewrite += script_code;

                rewrite = rewrite.Replace("@", "" + System.Environment.NewLine);

                System.IO.Directory.CreateDirectory(path + "converted\\");
                var File = System.IO.File.OpenWrite(path + "converted\\" + script_name + ".gml");

                File.Close();

                System.IO.File.WriteAllText(path + "converted\\" + script_name + ".gml", rewrite);

                //Form.log_text.Add(System.IO.File.ReadAllText(path + "converted\\" + name + ".sprite.gmx") +
                //    "\n has been output to file " + name + ".sprite.gmx" + ", path " + path + "converted");
            }
        }

        public static string[] Get_Files(string path)
        {
            string[] files = Directory.GetFiles(path);

            return files;
        }
    }

    static class Fonts
    {
        public static void Convert_Fonts_From_Path(string path)
        {
            string[] fonts = Fonts.Get_Files(path);
            foreach (string font in fonts)
            {
                if (Path.GetExtension(Path.GetFileName(font)) == ".xml")
                {
                    Fonts.Convert_Font(path, Path.GetFileName(font));
                }
            }
            Form.running = false;
            Form.log_text.Add("Finished font conversion." + System.Environment.NewLine + fonts.Length + " files processed.");
        }

        public static void Create_Font(int x, int y, int width, int height, string path, string file, int tex)
        {
            // Create a Bitmap object from a file.
            using (var myBitmap = new Bitmap(path + @"textures\texture_" + Convert.ToString(tex) + ".png"))
            {

                // Clone a portion of the Bitmap object.
                //Rectangle cloneRect = new Rectangle(0, 0, 100, 100);
                var format = myBitmap.PixelFormat;

                //Bitmap cloneBitmap = myBitmap.Clone(cloneRect, format);
                if (width + x > myBitmap.Width)
                {
                    width = width - 1 - (width - myBitmap.Width);
                }

                if (height + y > myBitmap.Height)
                {
                    height = height - 1 - (height - myBitmap.Height);
                }

                File.WriteAllText(@path + @"fonts\converted\images\data.txt", "Name: " + file + "   Width: " + Convert.ToString(width + x) + "   Height: " + Convert.ToString(height + y) + "   Tex width: " + Convert.ToString(myBitmap.Width) + "   Tex height: " + Convert.ToString(myBitmap.Height));

                Rectangle rect = new Rectangle(x, y, width, height);

                using (var spriteImage = myBitmap.Clone(rect, format))
                {
                    spriteImage.Save(@path + @"fonts\converted\images\" + @file);
                }

            }
        }

        public static void Create_Fonts(string path)
        {
            string[] fonts = Backgrounds.Get_Files(path);
            int i = 0;
            List<string> tex = new List<string> { };
            List<string> x = new List<string> { };
            List<string> y = new List<string> { };
            List<string> width_spr = new List<string> { };
            List<string> height_spr = new List<string> { };
            System.IO.Directory.CreateDirectory(path + @"converted\");
            System.IO.Directory.CreateDirectory(path + @"converted\images\");

            foreach (string font in fonts)
            {
                if (Path.GetExtension(Path.GetFileName(font)) == ".xml")
                {
                    string text = System.IO.File.ReadAllText(path + Path.GetFileName(font));

                    // INDEX
                    int index_a = text.LastIndexOf("<index>") + "<index>".Length;
                    int index_b = text.LastIndexOf("</index>");
                    string index = text.Substring(index_a, index_b - index_a);
                    //MessageBox.Show(text.Remove(text.LastIndexOf("<index>"), (text.LastIndexOf("</index>") + "</index>".Length)+1 - text.LastIndexOf("<index>") + "<index>".Length));
                    //MessageBox.Show(text.Remove(text.LastIndexOf("<index>"), (text.LastIndexOf("</index>") + "</index>".Length) - text.LastIndexOf("<index>")));
                    text = text.Remove(text.LastIndexOf("<index>"), (text.LastIndexOf("</index>") + "</index>".Length) - text.LastIndexOf("<index>"));

                    // X
                    int x_a = text.IndexOf("<X>") + "<X>".Length;
                    int x_b = text.IndexOf("</X>");
                    //MessageBox.Show("X" + a + "   a: " + x_a + "   b: " + x_b);
                    x.Add(text.Substring(x_a, x_b - x_a));
                    text = text.Remove(text.IndexOf("<X>"), (text.IndexOf("</X>") + "</X>".Length) - text.IndexOf("<X>"));

                    // Y
                    int y_a = text.IndexOf("<Y>") + "<Y>".Length;
                    int y_b = text.IndexOf("</Y>");
                    //MessageBox.Show("a: " + y_a + "   b: " + y_b);
                    y.Add(text.Substring(y_a, y_b - y_a));
                    text = text.Remove(text.IndexOf("<Y>"), (text.IndexOf("</Y>") + "</Y>".Length) - text.IndexOf("<Y>"));

                    // WIDTH
                    int width_spr_a = text.IndexOf("<Width>") + "<Width>".Length;
                    int width_spr_b = text.IndexOf("</Width>");
                    //MessageBox.Show("a: " + width_spr_a + "   b: " + width_spr_b);
                    width_spr.Add(text.Substring(width_spr_a, width_spr_b - width_spr_a));
                    text = text.Remove(text.IndexOf("<Width>"), (text.IndexOf("</Width>") + "</Width>".Length) - text.IndexOf("<Width>"));

                    // HEIGHT
                    int height_spr_a = text.IndexOf("<Height>") + "<Height>".Length;
                    int height_spr_b = text.IndexOf("</Height>");
                    //MessageBox.Show("a: " + height_spr_a + "   b: " + height_spr_b);
                    height_spr.Add(text.Substring(height_spr_a, height_spr_b - height_spr_a));
                    text = text.Remove(text.IndexOf("<Height>"), (text.IndexOf("</Height>") + "</Height>".Length) - text.IndexOf("<Height>"));

                    // TEXTURE NUMBER
                    int tex_a = text.LastIndexOf("<Texture_Index>") + "<Texture_Index>".Length;
                    int tex_b = text.LastIndexOf("</Texture_Index>");
                    //MessageBox.Show("a: " + tex_a + "   b: " + tex_b);
                    tex.Add(text.Substring(tex_a, tex_b - tex_a));
                    text = text.Remove(text.LastIndexOf("<Texture_Index>"), (text.LastIndexOf("</Texture_Index>") + "</Texture_Index>".Length) - text.LastIndexOf("<Texture_Index>"));

                    // MessageBox.Show(path.Remove(path.Length - @"sprites\".Length));

                    /*if (Convert.ToInt32(index) <= 0)
                    {*/
                        Fonts.Create_Font(Convert.ToInt32(x[0]), Convert.ToInt32(y[0]), Convert.ToInt32(width_spr[0]), Convert.ToInt32(height_spr[0]), path.Remove(path.Length - @"fonts\".Length), Path.GetFileName(font).Remove(Path.GetFileName(font).Length - 4) + ".png", Convert.ToInt32(tex[0]));
                    /*}
                    else
                    {
                        i = 0;
                        for (int v = Convert.ToInt32(index) + 1; v > 0; v--)
                        {
                            Backgrounds.Create_Background(Convert.ToInt32(x[i]), Convert.ToInt32(y[i]), Convert.ToInt32(width_spr[i]), Convert.ToInt32(height_spr[i]), path.Remove(path.Length - @"fonts\".Length), Path.GetFileName(font).Remove(Path.GetFileName(font).Length - 4) + "_" + Convert.ToString(i) + ".png", Convert.ToInt32(tex[i]));
                            i += 1;
                        }
                    }*/
                    tex.Clear();
                    x.Clear();
                    y.Clear();
                    width_spr.Clear();
                    height_spr.Clear();
                }
            }
            Form.running = false;
        }

        public static void Convert_Font(string path, string file)
        {
            string text = System.IO.File.ReadAllText(path + file);

            //System.Console.WriteLine("Contents of WriteText.txt = {0}", text);

            // GENERAL INFORMATION
            // NAME
            int name_a = text.IndexOf("<name>") + "<name>".Length;
            int name_b = text.IndexOf("</name>");
            string name = text.Substring(name_a, name_b - name_a);
            System.Console.WriteLine("Name: {0}", name);

            // SIZE
            int size_a = text.IndexOf("<Size>") + "<Size>".Length;
            int size_b = text.IndexOf("</Size>");
            string size = text.Substring(size_a, size_b - size_a);
            System.Console.WriteLine("Size: {0}", size);

            // BOLD
            int bold_a = text.IndexOf("<Bold>") + "<Bold>".Length;
            int bold_b = text.IndexOf("</Bold>");
            string bold = text.Substring(bold_a, bold_b - bold_a);
            System.Console.WriteLine("Bold: {0}", bold);

            // ITALIC
            int italic_a = text.IndexOf("<Italic>") + "<Italic>".Length;
            int italic_b = text.IndexOf("</Italic>");
            string italic = text.Substring(italic_a, italic_b - italic_a);
            System.Console.WriteLine("Bold: {0}", italic);

            // CHARSET
            int charset_a = text.IndexOf("<CharSet>") + "<CharSet>".Length;
            int charset_b = text.IndexOf("</CharSet>");
            string charset = text.Substring(charset_a, charset_b - charset_a);
            System.Console.WriteLine("Bold: {0}", charset);

            // FIRSTCHAR
            int firstchar_a = text.IndexOf("<FirstChar>") + "<FirstChar>".Length;
            int firstchar_b = text.IndexOf("</FirstChar>");
            string firstchar = text.Substring(firstchar_a, firstchar_b - firstchar_a);
            System.Console.WriteLine("First Charcter: {0}", firstchar);

            // ANTI ALIAS
            int anti_alias_a = text.IndexOf("<AntiAlias>") + "<AntiAlias>".Length;
            int anti_alias_b = text.IndexOf("</AntiAlias>");
            string anti_alias = text.Substring(anti_alias_a, anti_alias_b - anti_alias_a);
            System.Console.WriteLine("Anti Alias: {0}", anti_alias);

            // WIDTH
            int width_a = text.IndexOf("<Width>") + "<Width>".Length;
            int width_b = text.IndexOf("</Width>");
            string width = text.Substring(width_a, width_b - width_a);
            System.Console.WriteLine("Width: {0}", width);

            // HEIGHT
            int height_a = text.IndexOf("<Height>") + "<Height>".Length;
            int height_b = text.IndexOf("</Height>");
            string height = text.Substring(height_a, height_b - height_a);
            System.Console.WriteLine("Height: {0}", height);

            // GLYPHS
            // GLYPH AMOUNT
            int glyph_index_a = text.LastIndexOf("<index>") + "<index>".Length;
            int glyph_index_b = text.LastIndexOf("</index>");
            string glyph_index = text.Substring(glyph_index_a, glyph_index_b - glyph_index_a);
            System.Console.WriteLine("Glyph Index: {0}", glyph_index);

            // GLYPHS
            int glyphs_a = text.IndexOf("<Glyphs>") + "<Glyphs>".Length;
            int glyphs_b = text.IndexOf("</Glyphs>");
            string glyphs = text.Substring(glyphs_a, glyphs_b - glyphs_a);
            System.Console.WriteLine("Glyphs: {0}", glyphs);

            // GLYPH COLLECTIONS
            int[] glyph_col_a = new int[500];
            int[] glyph_col_b = new int[500];
            string[] glyph_col = new string[500];
            string glyphs_col = glyphs;

            // GLYPH X COLLECTIONS
            int[] glyph_col_x_a = new int[500];
            int[] glyph_col_x_b = new int[500];
            string[] glyph_col_x = new string[500];

            // GLYPH Y COLLECTIONS
            int[] glyph_col_y_a = new int[500];
            int[] glyph_col_y_b = new int[500];
            string[] glyph_col_y = new string[500];

            // GLYPH W COLLECTIONS
            int[] glyph_col_w_a = new int[500];
            int[] glyph_col_w_b = new int[500];
            string[] glyph_col_w = new string[500];

            // GLYPH H COLLECTIONS
            int[] glyph_col_h_a = new int[500];
            int[] glyph_col_h_b = new int[500];
            string[] glyph_col_h = new string[500];

            // GLYPH SHIFT COLLECTIONS
            int[] glyph_col_shift_a = new int[500];
            int[] glyph_col_shift_b = new int[500];
            string[] glyph_col_shift = new string[500];
            
            // GLYPH OFFSET COLLECTIONS
            int[] glyph_col_offset_a = new int[500];
            int[] glyph_col_offset_b = new int[500];
            string[] glyph_col_offset = new string[500];

            for (var i = 0; i < Convert.ToInt32(glyph_index) + 1; i++)
            {
                glyph_col_a[i] = glyphs_col.IndexOf("<File.Font.Glyph>") + "<File.Font.Glyph>".Length;
                glyph_col_b[i] = glyphs_col.IndexOf("</File.Font.Glyph>");
                glyph_col[i] = glyphs_col.Substring(glyph_col_a[i], glyph_col_b[i] - glyph_col_a[i]);
                glyphs_col = glyphs_col.Remove(0, glyphs_col.IndexOf("</File.Font.Glyph>") + "</File.Font.Glyph>".Length);
                System.Console.WriteLine("Glyph Collection {1}: {0}", glyph_col[i], i);

                // GLYPH X COLLECTIONS
                glyph_col_x_a[i] = glyph_col[i].IndexOf("<x>") + "<x>".Length;
                glyph_col_x_b[i] = glyph_col[i].IndexOf("</x>");
                glyph_col_x[i] = glyph_col[i].Substring(glyph_col_x_a[i], glyph_col_x_b[i] - glyph_col_x_a[i]);

                // GLYPH Y COLLECTIONS
                glyph_col_y_a[i] = glyph_col[i].IndexOf("<y>") + "<y>".Length;
                glyph_col_y_b[i] = glyph_col[i].IndexOf("</y>");
                glyph_col_y[i] = glyph_col[i].Substring(glyph_col_y_a[i], glyph_col_y_b[i] - glyph_col_y_a[i]);

                // GLYPH W COLLECTIONS
                glyph_col_w_a[i] = glyph_col[i].IndexOf("<width>") + "<width>".Length;
                glyph_col_w_b[i] = glyph_col[i].IndexOf("</width>");
                glyph_col_w[i] = glyph_col[i].Substring(glyph_col_w_a[i], glyph_col_w_b[i] - glyph_col_w_a[i]);

                // GLYPH H COLLECTIONS
                glyph_col_h_a[i] = glyph_col[i].IndexOf("<height>") + "<height>".Length;
                glyph_col_h_b[i] = glyph_col[i].IndexOf("</height>");
                glyph_col_h[i] = glyph_col[i].Substring(glyph_col_h_a[i], glyph_col_h_b[i] - glyph_col_h_a[i]);

                // GLYPH SHIFT COLLECTIONS
                glyph_col_shift_a[i] = glyph_col[i].IndexOf("<shift>") + "<shift>".Length;
                glyph_col_shift_b[i] = glyph_col[i].IndexOf("</shift>");
                glyph_col_shift[i] = glyph_col[i].Substring(glyph_col_shift_a[i], glyph_col_shift_b[i] - glyph_col_shift_a[i]);

                // GLYPH OFFSET COLLECTIONS
                glyph_col_offset_a[i] = glyph_col[i].IndexOf("<offset>") + "<offset>".Length;
                glyph_col_offset_b[i] = glyph_col[i].IndexOf("</offset>");
                glyph_col_offset[i] = glyph_col[i].Substring(glyph_col_offset_a[i], glyph_col_offset_b[i] - glyph_col_offset_a[i]);
            }

                // REWRITE
                string rewrite = "";
            rewrite += "<!--This Document is generated by GameMaker, if you edit it by hand then you do so at your own risk!-->@";
            rewrite += "<font>@";
            rewrite += "    <name>" + name + "</name>@";
            rewrite += "    <size>" + size + "</size>@";
            rewrite += "    <bold>" + bold + "</bold>@";
            rewrite += "    <renderhq>" + "0" + "</renderhq>@";
            rewrite += "    <italic>" + italic + "</italic>@";
            rewrite += "    <charset>" + charset + "</charset>@";
            rewrite += "    <aa>" + anti_alias + "</aa>@";
            rewrite += "    <includeTTF>" + "0" + "</includeTTF>@";
            rewrite += "    <TTFName>" + "" + "</TTFName>@";
            rewrite += "    <textgroups>@" + "        <texgroup0>" + "0" + "</textgroup0>@" + "    </texgroups>@";
            rewrite += "    <ranges>@" + "        <range0>" + width + "," + height + "</range0>@" + "    </ranges>@";
            rewrite += "    <glyphs>@";

            for (var n = 0; n < Convert.ToInt32(glyph_index)+1; n++) {
                rewrite += "        <glyph character=\"" + Convert.ToInt32(firstchar) + n + "\" x=\"" + glyph_col_x[n] + "\" y=\"" + glyph_col_y[n] + "\" w=\"" + glyph_col_w[n] + "\" h=\"" + glyph_col_h[n] + "\" shift=\"" + glyph_col_shift[n] + "\" offset=\"" + glyph_col_offset[n] + "\"/>@";
            }

            rewrite += "    </glyphs>@";
            rewrite += "    <kerningPairs/>@";
            rewrite += "    <image>" + name + ".png" + "</image>@";
            rewrite += "</font>";

            rewrite = rewrite.Replace("@", "" + System.Environment.NewLine);

            System.IO.Directory.CreateDirectory(path + "converted\\");
            var File = System.IO.File.OpenWrite(path + "converted\\" + name + ".font.gmx");

            File.Close();

            System.IO.File.WriteAllText(path + "converted\\" + name + ".font.gmx", rewrite);

            //Form.log_text.Add(System.IO.File.ReadAllText(path + "converted\\" + name + ".sprite.gmx") +
            //    "\n has been output to file " + name + ".sprite.gmx" + ", path " + path + "converted");
        }

        public static string[] Get_Files(string path)
        {
            string[] files = Directory.GetFiles(path);

            return files;
        }
    }

    static class Backgrounds
    {
        public static void Convert_Backgrounds_From_Path(string path)
        {
            string[] backgrounds = Backgrounds.Get_Files(path);
            foreach (string background in backgrounds)
            {
                if (Path.GetExtension(Path.GetFileName(background)) == ".xml")
                {
                    Backgrounds.Convert_Background(path, Path.GetFileName(background));
                }
            }
            Form.running = false;
            Form.log_text.Add("Finished background conversion." + System.Environment.NewLine + backgrounds.Length + " files processed.");
        }

        public static void Create_Background(int x, int y, int width, int height, string path, string file, int tex)
        {
            // Create a Bitmap object from a file.
            using (var myBitmap = new Bitmap(path + @"textures\texture_" + Convert.ToString(tex) + ".png"))
            {

                // Clone a portion of the Bitmap object.
                //Rectangle cloneRect = new Rectangle(0, 0, 100, 100);
                var format = myBitmap.PixelFormat;

                //Bitmap cloneBitmap = myBitmap.Clone(cloneRect, format);
                if (width + x > myBitmap.Width)
                {
                    width = width - 1 - (width - myBitmap.Width);
                }

                if (height + y > myBitmap.Height)
                {
                    height = height - 1 - (height - myBitmap.Height);
                }

                File.WriteAllText(@path + @"backgrounds\converted\images\data.txt", "Name: " + file + "   Width: " + Convert.ToString(width + x) + "   Height: " + Convert.ToString(height + y) + "   Tex width: " + Convert.ToString(myBitmap.Width) + "   Tex height: " + Convert.ToString(myBitmap.Height));

                Rectangle rect = new Rectangle(x, y, width, height);

                using (var spriteImage = myBitmap.Clone(rect, format))
                {
                    spriteImage.Save(@path + @"backgrounds\converted\images\" + @file);
                }

            }
        }

        public static void Create_Backgrounds(string path)
        {
            string[] backgrounds = Backgrounds.Get_Files(path);
            int i = 0;
            List<string> tex = new List<string> { };
            List<string> x = new List<string> { };
            List<string> y = new List<string> { };
            List<string> width_spr = new List<string> { };
            List<string> height_spr = new List<string> { };
            System.IO.Directory.CreateDirectory(path + @"converted\");
            System.IO.Directory.CreateDirectory(path + @"converted\images\");

            foreach (string background in backgrounds)
            {
                if (Path.GetExtension(Path.GetFileName(background)) == ".xml")
                {
                    string text = System.IO.File.ReadAllText(path + Path.GetFileName(background));

                    // INDEX
                    int index_a = text.LastIndexOf("<index>") + "<index>".Length;
                    int index_b = text.LastIndexOf("</index>");
                    string index = text.Substring(index_a, index_b - index_a);
                    //MessageBox.Show(text.Remove(text.LastIndexOf("<index>"), (text.LastIndexOf("</index>") + "</index>".Length)+1 - text.LastIndexOf("<index>") + "<index>".Length));
                    //MessageBox.Show(text.Remove(text.LastIndexOf("<index>"), (text.LastIndexOf("</index>") + "</index>".Length) - text.LastIndexOf("<index>")));
                    text = text.Remove(text.LastIndexOf("<index>"), (text.LastIndexOf("</index>") + "</index>".Length) - text.LastIndexOf("<index>"));

                    // X
                        int x_a = text.LastIndexOf("<X>") + "<X>".Length;
                        int x_b = text.LastIndexOf("</X>");
                        //MessageBox.Show("X" + a + "   a: " + x_a + "   b: " + x_b);
                        x.Add(text.Substring(x_a, x_b - x_a));
                        text = text.Remove(text.LastIndexOf("<X>"), (text.LastIndexOf("</X>") + "</X>".Length) - text.LastIndexOf("<X>"));

                    // Y
                        int y_a = text.LastIndexOf("<Y>") + "<Y>".Length;
                        int y_b = text.LastIndexOf("</Y>");
                        //MessageBox.Show("a: " + y_a + "   b: " + y_b);
                        y.Add(text.Substring(y_a, y_b - y_a));
                        text = text.Remove(text.LastIndexOf("<Y>"), (text.LastIndexOf("</Y>") + "</Y>".Length) - text.LastIndexOf("<Y>"));

                    // WIDTH
                        int width_spr_a = text.LastIndexOf("<Width>") + "<Width>".Length;
                        int width_spr_b = text.LastIndexOf("</Width>");
                        //MessageBox.Show("a: " + width_spr_a + "   b: " + width_spr_b);
                        width_spr.Add(text.Substring(width_spr_a, width_spr_b - width_spr_a));
                        text = text.Remove(text.LastIndexOf("<Width>"), (text.LastIndexOf("</Width>") + "</Width>".Length) - text.LastIndexOf("<Width>"));

                    // HEIGHT
                        int height_spr_a = text.LastIndexOf("<Height>") + "<Height>".Length;
                        int height_spr_b = text.LastIndexOf("</Height>");
                        //MessageBox.Show("a: " + height_spr_a + "   b: " + height_spr_b);
                        height_spr.Add(text.Substring(height_spr_a, height_spr_b - height_spr_a));
                        text = text.Remove(text.LastIndexOf("<Height>"), (text.LastIndexOf("</Height>") + "</Height>".Length) - text.LastIndexOf("<Height>"));

                    // TEXTURE NUMBER
                        int tex_a = text.LastIndexOf("<Texture_Index>") + "<Texture_Index>".Length;
                        int tex_b = text.LastIndexOf("</Texture_Index>");
                        //MessageBox.Show("a: " + tex_a + "   b: " + tex_b);
                        tex.Add(text.Substring(tex_a, tex_b - tex_a));
                        text = text.Remove(text.LastIndexOf("<Texture_Index>"), (text.LastIndexOf("</Texture_Index>") + "</Texture_Index>".Length) - text.LastIndexOf("<Texture_Index>"));

                    // MessageBox.Show(path.Remove(path.Length - @"sprites\".Length));

                    if (Convert.ToInt32(index) <= 0)
                    {
                        Backgrounds.Create_Background(Convert.ToInt32(x[0]), Convert.ToInt32(y[0]), Convert.ToInt32(width_spr[0]), Convert.ToInt32(height_spr[0]), path.Remove(path.Length - @"backgrounds\".Length), Path.GetFileName(background).Remove(Path.GetFileName(background).Length - 4) + ".png", Convert.ToInt32(tex[0]));
                    }
                    else
                    {
                        i = 0;
                        for (int v = Convert.ToInt32(index) + 1; v > 0; v--)
                        {
                            Backgrounds.Create_Background(Convert.ToInt32(x[i]), Convert.ToInt32(y[i]), Convert.ToInt32(width_spr[i]), Convert.ToInt32(height_spr[i]), path.Remove(path.Length - @"backgrounds\".Length), Path.GetFileName(background).Remove(Path.GetFileName(background).Length - 4) + "_" + Convert.ToString(i) + ".png", Convert.ToInt32(tex[i]));
                            i += 1;
                        }
                    }
                    tex.Clear();
                    x.Clear();
                    y.Clear();
                    width_spr.Clear();
                    height_spr.Clear();
                }
            }
            Form.running = false;
        }

        public static void Convert_Background(string path, string file)
        {
            string text = System.IO.File.ReadAllText(path + file);

            //System.Console.WriteLine("Contents of WriteText.txt = {0}", text);

            // GENERAL INFORMATION
            // NAME
            int name_a = text.IndexOf("<name>") + "<name>".Length;
            int name_b = text.IndexOf("</name>");
            string name = text.Substring(name_a, name_b - name_a);
            System.Console.WriteLine("Name: {0}", name);

            // HEIGHT
            int height_a = text.IndexOf("<Height>") + "<Height>".Length;
            int height_b = text.IndexOf("</Height>");
            string height = text.Substring(height_a, height_b - height_a);
            System.Console.WriteLine("Height: {0}", height);

            // WIDTH
            int width_a = text.IndexOf("<Width>") + "<Width>".Length;
            int width_b = text.IndexOf("</Width>");
            string width = text.Substring(width_a, width_b - width_a);
            System.Console.WriteLine("Name: {0}", width);

            // OFFSET X
            int xoff_a = text.IndexOf("<Offset_X>") + "<Offset_X>".Length;
            int xoff_b = text.IndexOf("</Offset_X>");
            string xoff = text.Substring(xoff_a, xoff_b - xoff_a);
            System.Console.WriteLine("X Offset: {0}", xoff);

            // OFFSET Y
            int yoff_a = text.IndexOf("<Offset_Y>") + "<Offset_Y>".Length;
            int yoff_b = text.IndexOf("</Offset_Y>");
            string yoff = text.Substring(yoff_a, yoff_b - yoff_a);
            System.Console.WriteLine("Y Offset: {0}", yoff);

            // TEXTURE INDEX
            int tex_a = text.IndexOf("<Texture_Index>") + "<Texture_Index>".Length;
            int tex_b = text.IndexOf("</Texture_Index>");
            string tex = text.Substring(tex_a, tex_b - tex_a);
            System.Console.WriteLine("Texture Index: {0}", tex);


            // REWRITE
            string rewrite = "";
            rewrite += "<!--This Document is generated by GameMaker, if you edit it by hand then you do so at your own risk!-->@";
            rewrite += "<background>@";
            rewrite += "    <istileset>" + "0" + "</istileset>@";
            rewrite += "    <tilewidth>" + "0" + "</tilewidth>@";
            rewrite += "    <tileheight>" + "0" + "</tileheight>@";
            rewrite += "    <tilexoff>" + xoff + "</tilexoff>@";
            rewrite += "    <tileyoff>" + yoff + "</tileyoff>@";
            rewrite += "    <tilehsep>" + "0" + "</tilehsep>@";
            rewrite += "    <tilevsep>" + "0" + "</tilevsep>@";
            rewrite += "    <HTile>" + "-1" + "</HTile>@";
            rewrite += "    <VTile>" + "-1" + "</VTile>@";
            rewrite += "    <TextureGroups>@";

            rewrite += "        <TextureGroup0>" + tex + "</TextureGroup0>@";

            rewrite += "    </TextureGroups>@";
            rewrite += "    <For3D>" + "0" + "</For3D>@";
            rewrite += "    <width>" + width + "</width>@";
            rewrite += "    <height>" + height + "</height>@";
            rewrite += "    <data>" + "images\\" + name + ".png" + "</data>@";
            rewrite += "</background>";


            rewrite = rewrite.Replace("@", "" + System.Environment.NewLine);

            System.IO.Directory.CreateDirectory(path + "converted\\");
            var File = System.IO.File.OpenWrite(path + "converted\\" + name + ".background.gmx");

            File.Close();

            System.IO.File.WriteAllText(path + "converted\\" + name + ".background.gmx", rewrite);

            //Form.log_text.Add(System.IO.File.ReadAllText(path + "converted\\" + name + ".sprite.gmx") +
            //    "\n has been output to file " + name + ".sprite.gmx" + ", path " + path + "converted");
        }

        public static string[] Get_Files(string path)
        {
            string[] files = Directory.GetFiles(path);

            return files;
        }
    }

    static class Sounds
    {
        public static void Convert_Sounds_From_Path(string path)
        {
            string[] sounds = Sounds.Get_Files(path);
            foreach (string sound in sounds)
            {
                if (Path.GetExtension(Path.GetFileName(sound)) == ".xml")
                {
                    Sounds.Convert_Sound(path, Path.GetFileName(sound));
                }
            }
            Form.running = false;
            Form.log_text.Add("Finished sound conversion." + System.Environment.NewLine + sounds.Length + " files processed.");
        }

        public static void Convert_Sound(string path, string file)
        {
            string text = System.IO.File.ReadAllText(path + file);

            //System.Console.WriteLine("Contents of WriteText.txt = {0}", text);

            // GENERAL INFORMATION
            // NAME
            int name_a = text.IndexOf("<name>") + "<name>".Length;
            int name_b = text.IndexOf("</name>");
            string name = text.Substring(name_a, name_b - name_a);
            System.Console.WriteLine("Name: {0}", name);

            // KIND
            int kind_a = text.IndexOf("<filename>") + "<filename>".Length;
            int kind_b = text.IndexOf("</filename>");
            string kind = text.Substring(kind_a, kind_b - kind_a).Remove(0, text.Substring(kind_a, kind_b - kind_a).Length-4);
            System.Console.WriteLine("Kind: {0}", kind);
            if (kind == ".wav") kind = "0";
            else if (kind == ".mp3") kind = "1";
            else if (kind == ".ogg") kind = "3";

            // EXTENSION
            int extension_a = text.IndexOf("<extension>") + "<extension>".Length;
            int extension_b = text.IndexOf("</extension>");
            string extension = text.Substring(extension_a, extension_b - extension_a);
            System.Console.WriteLine("Extension: {0}", extension);

            // FILENAME
            int filename_a = text.IndexOf("<filename>") + "<filename>".Length;
            int filename_b = text.IndexOf("</filename>");
            string filename = text.Substring(filename_a, filename_b - filename_a);
            System.Console.WriteLine("Filename: {0}", filename);

            // VOLUME
            int volume_a = text.IndexOf("<volume>") + "<volume>".Length;
            int volume_b = text.IndexOf("</volume>");
            string volume = text.Substring(volume_a, volume_b - volume_a);
            System.Console.WriteLine("Volume: {0}", volume);

            // PAN
            int pan_a = text.IndexOf("<pan>") + "<pan>".Length;
            int pan_b = text.IndexOf("</pan>");
            string pan = text.Substring(pan_a, pan_b - pan_a);
            System.Console.WriteLine("Pan: {0}", pan);

            // EFFECTS
            int effects_a = text.IndexOf("<effects>") + "<effects>".Length;
            int effects_b = text.IndexOf("</effects>");
            string effects = text.Substring(effects_a, effects_b - effects_a);
            System.Console.WriteLine("Effects: {0}", effects);

            // REWRITE
            string rewrite = "";
            rewrite += "<!--This Document is generated by GameMaker, if you edit it by hand then you do so at your own risk!-->@";
            rewrite += "<sound>@";
            rewrite += "    <kind>" + "3" + "</kind>@";
            rewrite += "    <extension>" + extension + "</extension>@";
            rewrite += "    <origname>" + "sound\\audio\\" + name + extension + "</origname>@";
            rewrite += "    <effects>" + effects + "</effects>@";
            rewrite += "    <volume>@" + "        <volume>" + volume + "</volume>@" + "    </volume>@";
            rewrite += "    <pan>" + pan + "</pan>@";
            rewrite += "    <bitRates>@" + "        <bitRate>" + "192" + "</bitRate>@" + "    </bitRates>@";
            rewrite += "    <sampleRates>@" + "        <sampleRate>" + "44100" + "</sampleRate>@" + "    </sampleRates>@";
            rewrite += "    <types>@" + "        <type>" + "0" + "</type>@" + "    </types>@";
            rewrite += "    <bitDepths>@" + "        <bitDepth>" + "16" + "</bitDepth>@" + "    </bitDepths>@";
            rewrite += "    <preload>" + "-1" + "</preload>@";
            rewrite += "    <data>" + name + extension + "</data>@";
            rewrite += "    <compressed>" + "0" + "</compressed>@";
            rewrite += "    <streamed>" + "0" + "</streamed>@";
            rewrite += "    <uncompressedOnLoad>" + "0" + "</uncompressedOnLoad>@";
            rewrite += "    <audioGroup>" + "0" + "</audioGroup>@";
            rewrite += "</sound>";


            rewrite = rewrite.Replace("@", "" + System.Environment.NewLine);

            System.IO.Directory.CreateDirectory(path + "converted\\");
            var File = System.IO.File.OpenWrite(path + "converted\\" + name + ".sound.gmx");

            File.Close();

            System.IO.File.WriteAllText(path + "converted\\" + name + ".sound.gmx", rewrite);

            //Form.log_text.Add(System.IO.File.ReadAllText(path + "converted\\" + name + ".sprite.gmx") +
            //    "\n has been output to file " + name + ".sprite.gmx" + ", path " + path + "converted");
        }

        public static string[] Get_Files(string path)
        {
            string[] files = Directory.GetFiles(path);

            return files;
        }
    }

    static class Sprites
    {
        public static void Convert_Sprites_From_Path(string path)
        {
            string[] sprites = Sprites.Get_Files(path);
            foreach (string sprite in sprites)
            {
                if (Path.GetExtension(Path.GetFileName(sprite)) == ".xml")
                {
                    Sprites.Convert_Sprite(path, Path.GetFileName(sprite));
                }
            }
            Form.running = false;
            Form.log_text.Add("Finished sprite conversion." + System.Environment.NewLine + sprites.Length + " files processed.");
        }

        public static void Create_Sprite(int x, int y, int width, int height, string path, string file, int tex)
        {
            // Create a Bitmap object from a file.
            using (var myBitmap = new Bitmap(path + @"textures\texture_" + Convert.ToString(tex) + ".png"))
            {

                // Clone a portion of the Bitmap object.
                //Rectangle cloneRect = new Rectangle(0, 0, 100, 100);
                var format = myBitmap.PixelFormat;

                //Bitmap cloneBitmap = myBitmap.Clone(cloneRect, format);
                if (width+x > myBitmap.Width)
                {
                    width = width-1 - ( width - myBitmap.Width );
                }

                if (height+y > myBitmap.Height)
                {
                    height = height - 1 - (height - myBitmap.Height);
                }

                File.WriteAllText(@path + @"sprites\converted\images\data.txt", "Name: " + file + "   Width: " + Convert.ToString(width+x) + "   Height: " + Convert.ToString(height+y) + "   Tex width: " + Convert.ToString(myBitmap.Width) + "   Tex height: " + Convert.ToString(myBitmap.Height));

                Rectangle rect = new Rectangle(x, y, width, height);

                using (var spriteImage = myBitmap.Clone(rect, format))
                {
                    spriteImage.Save(@path + @"sprites\converted\images\" + @file);
                }

            }
        }

        public static void Create_Sprites(string path)
        {
            string[] sprites = Sprites.Get_Files(path);
            int i = 0;
            List<string> tex = new List<string> { };
            List<string> x = new List<string> { };
            List<string> y = new List<string> { };
            List<string> width_spr = new List<string> { };
            List<string> height_spr = new List<string> { };
            System.IO.Directory.CreateDirectory(path + @"converted\");
            System.IO.Directory.CreateDirectory(path + @"converted\images\");

            foreach (string sprite in sprites)
            {
                if (Path.GetExtension(Path.GetFileName(sprite)) == ".xml")
                {
                    string text = System.IO.File.ReadAllText(path + Path.GetFileName(sprite));

                    // INDEX
                    int index_a = text.LastIndexOf("<index>") + "<index>".Length;
                    int index_b = text.LastIndexOf("</index>");
                    string index = text.Substring(index_a, index_b - index_a);
                    //MessageBox.Show(text.Remove(text.LastIndexOf("<index>"), (text.LastIndexOf("</index>") + "</index>".Length)+1 - text.LastIndexOf("<index>") + "<index>".Length));
                    //MessageBox.Show(text.Remove(text.LastIndexOf("<index>"), (text.LastIndexOf("</index>") + "</index>".Length) - text.LastIndexOf("<index>")));
                    text = text.Remove(text.LastIndexOf("<index>"), (text.LastIndexOf("</index>") + "</index>".Length) - text.LastIndexOf("<index>"));

                    // X
                    for (var a = Convert.ToInt32(index) + 1; a > 0; a--) {
                        int x_a = text.LastIndexOf("<X>") + "<X>".Length;
                        int x_b = text.LastIndexOf("</X>");
                        //MessageBox.Show("X" + a + "   a: " + x_a + "   b: " + x_b);
                        x.Add(text.Substring(x_a, x_b - x_a));
                        text = text.Remove(text.LastIndexOf("<X>"), (text.LastIndexOf("</X>") + "</X>".Length) - text.LastIndexOf("<X>"));
                    }

                    // Y
                    for (var b = Convert.ToInt32(index) + 1; b > 0; b--)
                    {
                        int y_a = text.LastIndexOf("<Y>") + "<Y>".Length;
                        int y_b = text.LastIndexOf("</Y>");
                        //MessageBox.Show("a: " + y_a + "   b: " + y_b);
                        y.Add(text.Substring(y_a, y_b - y_a));
                        text = text.Remove(text.LastIndexOf("<Y>"), (text.LastIndexOf("</Y>") + "</Y>".Length) - text.LastIndexOf("<Y>"));
                    }

                    // WIDTH
                    for (var c = Convert.ToInt32(index) + 1; c > 0; c--)
                    {
                        int width_spr_a = text.LastIndexOf("<Width>") + "<Width>".Length;
                        int width_spr_b = text.LastIndexOf("</Width>");
                        //MessageBox.Show("a: " + width_spr_a + "   b: " + width_spr_b);
                        width_spr.Add(text.Substring(width_spr_a, width_spr_b - width_spr_a));
                        text = text.Remove(text.LastIndexOf("<Width>"), (text.LastIndexOf("</Width>") + "</Width>".Length) - text.LastIndexOf("<Width>"));
                    }

                    // HEIGHT
                    for (var d = Convert.ToInt32(index) + 1; d > 0; d--)
                    {
                        int height_spr_a = text.LastIndexOf("<Height>") + "<Height>".Length;
                        int height_spr_b = text.LastIndexOf("</Height>");
                        //MessageBox.Show("a: " + height_spr_a + "   b: " + height_spr_b);
                        height_spr.Add(text.Substring(height_spr_a, height_spr_b - height_spr_a));
                        text = text.Remove(text.LastIndexOf("<Height>"), (text.LastIndexOf("</Height>") + "</Height>".Length) - text.LastIndexOf("<Height>"));
                    }

                    // TEXTURE NUMBER
                    for (var e = Convert.ToInt32(index) + 1; e > 0; e--)
                    {
                        int tex_a = text.LastIndexOf("<Texture_Index>") + "<Texture_Index>".Length;
                        int tex_b = text.LastIndexOf("</Texture_Index>");
                        //MessageBox.Show("a: " + tex_a + "   b: " + tex_b);
                        tex.Add(text.Substring(tex_a, tex_b - tex_a));
                        text = text.Remove(text.LastIndexOf("<Texture_Index>"), (text.LastIndexOf("</Texture_Index>") + "</Texture_Index>".Length) - text.LastIndexOf("<Texture_Index>"));
                    }

                    // MessageBox.Show(path.Remove(path.Length - @"sprites\".Length));

                    if (Convert.ToInt32(index) <= 0)
                    {
                        Sprites.Create_Sprite(Convert.ToInt32(x[0]), Convert.ToInt32(y[0]), Convert.ToInt32(width_spr[0]), Convert.ToInt32(height_spr[0]), path.Remove(path.Length - @"sprites\".Length), Path.GetFileName(sprite).Remove(Path.GetFileName(sprite).Length - 4) + ".png", Convert.ToInt32(tex[0]));
                    }
                    else
                    {
                        i = 0;
                        for (int v = Convert.ToInt32(index) + 1; v > 0; v--)
                        {
                            Sprites.Create_Sprite(Convert.ToInt32(x[i]), Convert.ToInt32(y[i]), Convert.ToInt32(width_spr[i]), Convert.ToInt32(height_spr[i]), path.Remove(path.Length - @"sprites\".Length), Path.GetFileName(sprite).Remove(Path.GetFileName(sprite).Length - 4) + "_" + Convert.ToString(i) + ".png", Convert.ToInt32(tex[i]));
                            i += 1;
                        }
                    }
                    tex.Clear();
                    x.Clear();
                    y.Clear();
                    width_spr.Clear();
                    height_spr.Clear();
                }
            }
            Form.running = false;
        }

        public static void Convert_Sprite(string path, string file)
        {
            string text = System.IO.File.ReadAllText(path + file);

            //System.Console.WriteLine("Contents of WriteText.txt = {0}", text);

            // GENERAL INFORMATION
            // WIDTH
            int width_a = text.IndexOf("<Width>") + "<Width>".Length;
            int width_b = text.IndexOf("</Width>");
            string width = text.Substring(width_a, width_b - width_a);
            System.Console.WriteLine("Width: {0}", width);

            // HEIGHT
            int height_a = text.IndexOf("<Height>") + "<Height>".Length;
            int height_b = text.IndexOf("</Height>");
            string height = text.Substring(height_a, height_b - height_a);
            System.Console.WriteLine("Height: {0}", height);

            // NAME
            int name_a = text.IndexOf("<name>") + "<name>".Length;
            int name_b = text.IndexOf("</name>");
            string name = text.Substring(name_a, name_b - name_a);
            System.Console.WriteLine("Name: {0}", name);

            // OTHER
            // TYPE
            int type_a = text.IndexOf("<Type>") + "<Type>".Length;
            int type_b = text.IndexOf("</Type>");
            string type = text.Substring(type_a, type_b - type_a);
            System.Console.WriteLine("Type: {0}", type);

            // INDEX
            int index_a = text.LastIndexOf("<index>") + "<index>".Length;
            int index_b = text.LastIndexOf("</index>");
            string index = text.Substring(index_a, index_b - index_a);
            System.Console.WriteLine("Indexes: {0}", index);

            // TEXTURENUM
            int tex_a = text.IndexOf("<Texture_Index>") + "<Texture_Index>".Length;
            int tex_b = text.IndexOf("</Texture_Index>");
            string tex = text.Substring(tex_a, tex_b - tex_a);
            System.Console.WriteLine("Tex: {0}", tex);

            // ORIGINALS
            // X ORIGINAL
            int orig_x_a = text.IndexOf("<Original_X>") + "<Original_X>".Length;
            int orig_x_b = text.IndexOf("</Original_X>");
            string orig_x = text.Substring(orig_x_a, orig_x_b - orig_x_a);
            System.Console.WriteLine("X origin: {0}", orig_x);

            // Y ORIGINAL
            int orig_y_a = text.IndexOf("<Original_Y>") + "<Original_Y>".Length;
            int orig_y_b = text.IndexOf("</Original_Y>");
            string orig_y = text.Substring(orig_y_a, orig_y_b - orig_y_a);
            System.Console.WriteLine("Y origin: {0}", orig_y);

            // COLLISION INFORMATION
            // LEFT
            int left_a = text.IndexOf("<Left>") + "<Left>".Length;
            int left_b = text.IndexOf("</Left>");
            string left = text.Substring(left_a, left_b - left_a);
            System.Console.WriteLine("Left: {0}", left);

            // RIGHT
            int right_a = text.IndexOf("<Right>") + "<Right>".Length;
            int right_b = text.IndexOf("</Right>");
            string right = text.Substring(right_a, right_b - right_a);
            System.Console.WriteLine("Right: {0}", right);

            // BOTTOM
            int bottom_a = text.IndexOf("<Bottom>") + "<Bottom>".Length;
            int bottom_b = text.IndexOf("</Bottom>");
            string bottom = text.Substring(bottom_a, bottom_b - bottom_a);
            System.Console.WriteLine("Bottom: {0}", bottom);

            // TOP
            int top_a = text.IndexOf("<Top>") + "<Top>".Length;
            int top_b = text.IndexOf("</Top>");
            string top = text.Substring(top_a, top_b - top_a);
            System.Console.WriteLine("Top: {0}", top);

            // PRECISE COLLISION
            int prec_col_a = text.IndexOf("<ColCheck>") + "<ColCheck>".Length;
            int prec_col_b = text.IndexOf("</ColCheck>");
            int prec_col = Convert.ToInt32(Convert.ToBoolean(text.Substring(prec_col_a, prec_col_b - prec_col_a)));
            System.Console.WriteLine("Precise Collision: {0}", prec_col);
            if (prec_col == 1) prec_col = -1;
            else prec_col = 0;

            // BOX MODE
            int mode_a = text.IndexOf("<Mode>") + "<Mode>".Length;
            int mode_b = text.IndexOf("</Mode>");
            string mode = text.Substring(mode_a, mode_b - mode_a);
            System.Console.WriteLine("Box Mode: {0}", mode);

            // COL KIND
            int col_kind_a = text.IndexOf("<has_masks>") + "<has_masks>".Length;
            int col_kind_b = text.IndexOf("</has_masks>");
            int col_kind = Convert.ToInt32(Convert.ToBoolean(text.Substring(col_kind_a, col_kind_b - col_kind_a)));
            System.Console.WriteLine("Collision Kind: {0}", col_kind);
            if (col_kind == 1) col_kind = 0;
            else col_kind = 1;

            // CREATE SPRITE
            int x_a = text.IndexOf("<X>") + "<X>".Length;
            int x_b = text.IndexOf("</X>");
            string x = text.Substring(x_a, x_b - x_a);

            int y_a = text.IndexOf("<Y>") + "<Y>".Length;
            int y_b = text.IndexOf("</Y>");
            string y = text.Substring(y_a, y_b - y_a);

            // WIDTH
            int width_spr_a = text.IndexOf("<Width>") + "<Width>".Length;
            int width_spr_b = text.IndexOf("</Width>");
            string width_spr = text.Substring(width_spr_a, width_spr_b - width_spr_a);
            System.Console.WriteLine("Width: {0}", width_spr);

            // HEIGHT
            int height_spr_a = text.LastIndexOf("<Height>") + "<Height>".Length;
            int height_spr_b = text.LastIndexOf("</Height>");
            string height_spr = text.Substring(height_spr_a, height_spr_b - height_spr_a);
            System.Console.WriteLine("Height: {0}", height_spr);

            if (Convert.ToInt32(index) > -1)
            {
                for (int v = Convert.ToInt32(index); v > -1; v--)
                {
                    Sprites.Create_Sprite(Convert.ToInt32(x), Convert.ToInt32(y), Convert.ToInt32(width_spr), Convert.ToInt32(height_spr), path.Remove(path.Length - "sprites\\".Length), name + "_" + Convert.ToString(Convert.ToInt32(index) - v) + ".png", Convert.ToInt32(tex));
                }
            }
            else
            {
                Sprites.Create_Sprite(Convert.ToInt32(x), Convert.ToInt32(y), Convert.ToInt32(width_spr), Convert.ToInt32(height_spr), path.Remove(path.Length - "sprites\\".Length), name + ".png", Convert.ToInt32(tex));
            }


            // REWRITE
            string rewrite = "";
            rewrite += "<!--This Document is generated by GameMaker, if you edit it by hand then you do so at your own risk!-->@";
            rewrite += "<sprite>@";
            rewrite += "    <type>" + type + "</type>@";
            rewrite += "    <xorig>" + orig_x + "</xorig>@";
            rewrite += "    <yorigin>" + orig_y + "</yorigin>@";
            rewrite += "    <colkind>" + col_kind + "</colkind>@";
            rewrite += "    <coltolerance>" + "0" + "</coltolerance>@";
            rewrite += "    <sepmasks>" + prec_col + "</sepmasks>@";
            rewrite += "    <bboxmode>" + mode + "</bboxmode>@";
            rewrite += "    <bbox_left>" + left + "</bbox_left>@";
            rewrite += "    <bbox_right>" + right + "</bbox_right>@";
            rewrite += "    <bbox_top>" + top + "</bbox_top>@";
            rewrite += "    <bbox_bottom>" + bottom + "</bbox_bottom>@";
            rewrite += "    <HTile>" + "0" + "</HTile>@";
            rewrite += "    <VTile>" + "0" + "</VTile>@";
            rewrite += "    <TextureGroups>@";

            rewrite += "        <TextureGroup0>" + tex + "</TextureGroup0>@";

            rewrite += "    </TextureGroups>@";
            rewrite += "    <For3D>" + "0" + "</For3D>@";
            rewrite += "    <width>" + width + "</width>@";
            rewrite += "    <height>" + height + "</height>@";
            rewrite += "    <frames>@";

            if (Convert.ToInt32(index) > 0)
            {
                for (int i = Convert.ToInt32(index); i > -1; i--)
                {
                    rewrite += "        <frame index=" + "\"" + Convert.ToString(Convert.ToInt32(index) - i) +
                        "\"" + ">images\\" + name + "_" + Convert.ToString(Convert.ToInt32(index) - i) + ".png" + "</frame>@";
                }
            }
            else
            {
                rewrite += "        <frame index=" + "\"" + 0 +
                        "\"" + ">images\\" + name + ".png" + "</frame>@";
            }

            rewrite += "    </frames>@";
            rewrite += "</sprite>";

            rewrite = rewrite.Replace("@", "" + System.Environment.NewLine);

            System.IO.Directory.CreateDirectory(path + "converted\\");
            var File = System.IO.File.OpenWrite(path + "converted\\" + name + ".sprite.gmx");

            File.Close();

            System.IO.File.WriteAllText(path + "converted\\" + name + ".sprite.gmx", rewrite);

            //Form.log_text.Add(System.IO.File.ReadAllText(path + "converted\\" + name + ".sprite.gmx") +
            //    "\n has been output to file " + name + ".sprite.gmx" + ", path " + path + "converted");
        }

        public static string[] Get_Files(string path)
        {
            string[] files = Directory.GetFiles(path);
            
            return files;
        }
    }
}
