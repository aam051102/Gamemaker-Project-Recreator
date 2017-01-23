using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamemaker_Recompiler_Visual
{
    static class Objects
    {
        public static void Convert_Objects_From_Path(string path)
        {
            string[] objects = Objects.Get_Files(path);
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

        public static string[] evNames =
        {
            "ev_step_normal",
            "ev_step_begin",
            "ev_step_end",
            "ev_create",
            "ev_alarm[0]",
            "ev_alarm[1]",
            "ev_alarm[2]",
            "ev_alarm[3]",
            "ev_alarm[4]",
            "ev_alarm[5]",
            "ev_alarm[6]",
            "ev_alarm[7]",
            "ev_alarm[8]",
            "ev_alarm[9]",
            "ev_alarm[10]",
            "ev_alarm[11]",
            "ev_draw",          // decompiler bug, ev_outside will get changed to this if it contains "draw_". will not work always, but most of the time
            "ev_outside",       // special
            "ev_destroy",
            "ev_collision",     // special
            "ev_keyboard",      // special
            "ev_user0",
            "ev_user1",
            "ev_user2",
            "ev_user3",
            "ev_user4",
            "ev_user5",
            "ev_user6",
            "ev_user7",
            "ev_user8",
            "ev_user9",
            "ev_user10",
            "ev_user11",
            "ev_user12",
            "ev_user13",
            "ev_user14",
            "ev_user15"
        };

        public static int[] evNumType =
        {
            3,
            3,
            3,
            0,
            2,
            2,
            2,
            2,
            2,
            2,
            2,
            2,
            2,
            2,
            2,
            2,
            8,
            7,
            1,
            4,
            5,
            7,
            7,
            7,
            7,
            7,
            7,
            7,
            7,
            7,
            7,
            7,
            7,
            7,
            7,
            7,
            7
        };

        public static int[] evNum =
        {
            0,
            0,
            0,
            0,
            0,
            1,
            2,
            3,
            4,
            5,
            6,
            7,
            8,
            9,
            10,
            11,
            0,
            0,
            0,
            -1,
            -1,
            10,
            11,
            12,
            13,
            14,
            15,
            16,
            17,
            18,
            19,
            20,
            21,
            22,
            23,
            24,
            25
        };

        public static void Convert_Object(string path, string file)
        {
            string text = System.IO.File.ReadAllText(path + file);
            string[] lines = System.IO.File.ReadAllLines(path + file);

            //System.Console.WriteLine("Contents of WriteText.txt = {0}", text);
            if (text != "")
            {
                // GENERAL INFORMATION
                // GENERAL OBJECT INFO
                int object_info_a = text.IndexOf("Object:") + "Object:".Length;
                int object_info_b = text.IndexOf("/*");
                string object_info = text.Substring(object_info_a, object_info_b - object_info_a);

                //MessageBox.Show("OBJECT");

                // NAME
                int name_a = object_info.IndexOf("builtin.name = \"") + "builtin.name = \"".Length;
                int name_b = object_info.Remove(0, name_a).IndexOf("\"") + name_a;
                //MessageBox.Show("A: " + Convert.ToString(name_a) + "   B: " + Convert.ToString(name_b) + System.Environment.NewLine + "   TEXT: " + object_info.Substring(name_a, name_b - name_a));
                string name = object_info.Substring(name_a, name_b - name_a);

                //MessageBox.Show("NAME");

                // SPRITE
                int sprite_a = object_info.IndexOf("builtin.sprite_index = ") + "builtin.sprite_index = ".Length;
                int sprite_b = object_info.Remove(0, sprite_a).IndexOf("builtin.") + sprite_a;
                string sprite = object_info.Substring(sprite_a, sprite_b - sprite_a);
                if (sprite.StartsWith("-1")) sprite = "";
                else
                {
                    sprite = sprite.Remove(0, sprite.IndexOf("\"") + 1);
                    sprite = sprite.Substring(0, sprite.IndexOf("\""));
                }

                //MessageBox.Show("SPRITE");

                // VISIBLE
                int visible_a = object_info.IndexOf("builtin.visible = ") + "builtin.visible = ".Length;
                int visible_b = object_info.IndexOf("builtin.") + visible_a;
                //MessageBox.Show(Convert.ToString(Convert.ToInt32(Convert.ToBoolean(object_info.Substring(visible_a, visible_b - visible_a)))));
                int visible = Convert.ToInt32(Convert.ToBoolean(object_info.Substring(visible_a, visible_b - visible_a)));
                if (visible == 1) visible = -1;

                //MessageBox.Show("VISIBLE");

                // SOLID
                int solid_a = object_info.IndexOf("builtin.solid = ") + "builtin.solid = ".Length;
                int solid_b = object_info.IndexOf("builtin.") + solid_a;
                int solid = Convert.ToInt32(Convert.ToBoolean(object_info.Substring(solid_a, solid_b - solid_a)));
                if (solid == 1) solid = -1;

                //MessageBox.Show("SOLID");

                // PERSISTENT
                int persistent_a = object_info.IndexOf("builtin.persistent = ") + "builtin.persistent = ".Length;
                int persistent_b = object_info.IndexOf("builtin.") + persistent_a;
                int persistent = Convert.ToInt32(Convert.ToBoolean(object_info.Substring(persistent_a, persistent_b - persistent_a)));
                if (persistent == 1) persistent = -1;

                //MessageBox.Show("PERSISTENT");

                // DEPTH
                int depth_a = text.IndexOf("builtin.depth = ") + "builtin.depth = ".Length;
                int depth_b = text.IndexOf("/*");
                int depth = Convert.ToInt32(text.Substring(depth_a, depth_b - depth_a));
                if (depth == 1) depth = -1;

                //MessageBox.Show("DEPTH");

                // PARENT NAME
                string parent_name = "&lt;undefined&gt;";
                if (object_info.IndexOf("builtin.parent_name = \"") != -1)
                {
                    int parent_name_a = object_info.IndexOf("builtin.parent_name = ") + "builtin.parent_name = ".Length;
                    int parent_name_b = object_info.Remove(0, parent_name_a).IndexOf("builtin.") + parent_name_a;
                    parent_name = object_info.Substring(parent_name_a, parent_name_b - parent_name_a);

                    parent_name = parent_name.Remove(0, parent_name.IndexOf("\"") + 1);
                    parent_name = parent_name.Substring(0, parent_name.IndexOf("\""));
                }

                //MessageBox.Show("PARENT");

                // MASK NAME
                string mask_name = "&lt;undefined&gt;";
                if (object_info.IndexOf("builtin.mask_name = \"") != -1)
                {
                    int mask_name_a = object_info.IndexOf("builtin.mask_name = ") + "builtin.mask_name = ".Length;
                    int mask_name_b = object_info.Remove(0, mask_name_a).IndexOf("builtin.") + mask_name_a;
                    mask_name = object_info.Substring(mask_name_a, mask_name_b - mask_name_a);

                    mask_name = mask_name.Remove(0, mask_name.IndexOf("\"") + 1);
                    mask_name = mask_name.Substring(0, mask_name.IndexOf("\""));
                }

                //string name = file.Remove(file.Length-3);

                // CODE



                // REWRITE
                string rewrite = "";
                rewrite += "<!--This Document is generated by GameMaker Project Recreater, if you edit it by hand then you do so at your own risk!-->@";
                rewrite += "<object>@";
                rewrite += "    <spriteName>" + sprite + "</spriteName>@";
                rewrite += "    <solid>" + solid + "</solid>@";
                rewrite += "    <visible>" + visible + "</visible>@";
                rewrite += "    <depth>" + depth + "</depth>@";
                rewrite += "    <persistent>" + persistent + "</persistent>@";
                rewrite += "    <parentName>" + parent_name + "</parentName>@";
                rewrite += "    <maskName>" + mask_name + "</maskName>@";

                if (code_index != 0)
                {
                    rewrite += "    <events>@";

                    for (var n = 1; n < code_index + 1; n++)
                    {
                        if (code_event_name[n].IndexOf("[") != -1)
                        {
                            if (CultureInfo.CurrentCulture.TextInfo.ToTitleCase(code_event_name[n].Remove(0, 2)).Substring(0, code_event_name[n].IndexOf("[") - (code_event_name[n].IndexOf("]") - code_event_name[n].IndexOf("["))) == "_Outside")
                            {
                                rewrite += "        <event eventtype=\"" + Event_To_Number("___Other") + "\" enumb=\"" + "0" + "\">@";
                            }
                            else
                            {
                                rewrite += "        <event eventtype=\"" + Event_To_Number(code_event_name[n]) + "\" enumb=\"" + code_event_name[n].Substring(code_event_name[n].IndexOf("[") + 1, code_event_name[n].IndexOf("]") - (code_event_name[n].IndexOf("[") + 1)) + "\">@";
                            }
                        }
                        else
                        {
                            // STEP
                            if (CultureInfo.CurrentCulture.TextInfo.ToTitleCase(code_event_name[n].Remove(0, 2)) == "_Step_Normal")
                            {
                                rewrite += "        <event eventtype=\"" + Event_To_Number("___Step") + "\" enumb=\"" + "0" + "\">@";
                            }
                            else if (CultureInfo.CurrentCulture.TextInfo.ToTitleCase(code_event_name[n].Remove(0, 2)) == "_Step_Begin")
                            {
                                rewrite += "        <event eventtype=\"" + Event_To_Number("___Step") + "\" enumb=\"" + "1" + "\">@";
                            }
                            else if (CultureInfo.CurrentCulture.TextInfo.ToTitleCase(code_event_name[n].Remove(0, 2)) == "_Step_End")
                            {
                                rewrite += "        <event eventtype=\"" + Event_To_Number("___Step") + "\" enumb=\"" + "2" + "\">@";
                            }
                            // OTHER
                            else if (CultureInfo.CurrentCulture.TextInfo.ToTitleCase(code_event_name[n].Remove(0, 2)) == "_Outside")
                            {
                                rewrite += "        <event eventtype=\"" + Event_To_Number("___Other") + "\" enumb=\"" + "0" + "\">@";
                            }
                            else if (CultureInfo.CurrentCulture.TextInfo.ToTitleCase(code_event_name[n].Remove(0, 2)) == "_User0")
                            {
                                rewrite += "        <event eventtype=\"" + Event_To_Number("___Other") + "\" enumb=\"" + "10" + "\">@";
                            }
                            else if (CultureInfo.CurrentCulture.TextInfo.ToTitleCase(code_event_name[n].Remove(0, 2)) == "_User1")
                            {
                                rewrite += "        <event eventtype=\"" + Event_To_Number("___Other") + "\" enumb=\"" + "11" + "\">@";
                            }
                            else if (CultureInfo.CurrentCulture.TextInfo.ToTitleCase(code_event_name[n].Remove(0, 2)) == "_User2")
                            {
                                rewrite += "        <event eventtype=\"" + Event_To_Number("___Other") + "\" enumb=\"" + "12" + "\">@";
                            }
                            else if (CultureInfo.CurrentCulture.TextInfo.ToTitleCase(code_event_name[n].Remove(0, 2)) == "_User3")
                            {
                                rewrite += "        <event eventtype=\"" + Event_To_Number("___Other") + "\" enumb=\"" + "13" + "\">@";
                            }
                            else if (CultureInfo.CurrentCulture.TextInfo.ToTitleCase(code_event_name[n].Remove(0, 2)) == "_User4")
                            {
                                rewrite += "        <event eventtype=\"" + Event_To_Number("___Other") + "\" enumb=\"" + "14" + "\">@";
                            }
                            else if (CultureInfo.CurrentCulture.TextInfo.ToTitleCase(code_event_name[n].Remove(0, 2)) == "_User5")
                            {
                                rewrite += "        <event eventtype=\"" + Event_To_Number("___Other") + "\" enumb=\"" + "15" + "\">@";
                            }
                            else if (CultureInfo.CurrentCulture.TextInfo.ToTitleCase(code_event_name[n].Remove(0, 2)) == "_User6")
                            {
                                rewrite += "        <event eventtype=\"" + Event_To_Number("___Other") + "\" enumb=\"" + "16" + "\">@";
                            }
                            else if (CultureInfo.CurrentCulture.TextInfo.ToTitleCase(code_event_name[n].Remove(0, 2)) == "_User7")
                            {
                                rewrite += "        <event eventtype=\"" + Event_To_Number("___Other") + "\" enumb=\"" + "17" + "\">@";
                            }
                            else if (CultureInfo.CurrentCulture.TextInfo.ToTitleCase(code_event_name[n].Remove(0, 2)) == "_User8")
                            {
                                rewrite += "        <event eventtype=\"" + Event_To_Number("___Other") + "\" enumb=\"" + "18" + "\">@";
                            }
                            else if (CultureInfo.CurrentCulture.TextInfo.ToTitleCase(code_event_name[n].Remove(0, 2)) == "_User9")
                            {
                                rewrite += "        <event eventtype=\"" + Event_To_Number("___Other") + "\" enumb=\"" + "19" + "\">@";
                            }
                            else if (CultureInfo.CurrentCulture.TextInfo.ToTitleCase(code_event_name[n].Remove(0, 2)) == "_User10")
                            {
                                rewrite += "        <event eventtype=\"" + Event_To_Number("___Other") + "\" enumb=\"" + "20" + "\">@";
                            }
                            else if (CultureInfo.CurrentCulture.TextInfo.ToTitleCase(code_event_name[n].Remove(0, 2)) == "_User11")
                            {
                                rewrite += "        <event eventtype=\"" + Event_To_Number("___Other") + "\" enumb=\"" + "21" + "\">@";
                            }
                            else if (CultureInfo.CurrentCulture.TextInfo.ToTitleCase(code_event_name[n].Remove(0, 2)) == "_User12")
                            {
                                rewrite += "        <event eventtype=\"" + Event_To_Number("___Other") + "\" enumb=\"" + "22" + "\">@";
                            }
                            else if (CultureInfo.CurrentCulture.TextInfo.ToTitleCase(code_event_name[n].Remove(0, 2)) == "_User13")
                            {
                                rewrite += "        <event eventtype=\"" + Event_To_Number("___Other") + "\" enumb=\"" + "23" + "\">@";
                            }
                            else if (CultureInfo.CurrentCulture.TextInfo.ToTitleCase(code_event_name[n].Remove(0, 2)) == "_User14")
                            {
                                rewrite += "        <event eventtype=\"" + Event_To_Number("___Other") + "\" enumb=\"" + "24" + "\">@";
                            }
                            else if (CultureInfo.CurrentCulture.TextInfo.ToTitleCase(code_event_name[n].Remove(0, 2)) == "_User15")
                            {
                                rewrite += "        <event eventtype=\"" + Event_To_Number("___Other") + "\" enumb=\"" + "25" + "\">@";
                            }
                            // MOUSE
                            else if (CultureInfo.CurrentCulture.TextInfo.ToTitleCase(code_event_name[n].Remove(0, 2)) == "_Mouse_Left_Button")
                            {
                                rewrite += "        <event eventtype=\"" + Event_To_Number("___Mouse") + "\" enumb=\"" + "0" + "\">@";
                            }
                            else if (CultureInfo.CurrentCulture.TextInfo.ToTitleCase(code_event_name[n].Remove(0, 2)) == "_Mouse_Right_Button")
                            {
                                rewrite += "        <event eventtype=\"" + Event_To_Number("___Mouse") + "\" enumb=\"" + "1" + "\">@";
                            }
                            else if (CultureInfo.CurrentCulture.TextInfo.ToTitleCase(code_event_name[n].Remove(0, 2)) == "_Mouse_Middle_Button")
                            {
                                rewrite += "        <event eventtype=\"" + Event_To_Number("___Mouse") + "\" enumb=\"" + "2" + "\">@";
                            }
                            else
                            {
                                rewrite += "        <event eventtype=\"" + Event_To_Number(code_event_name[n]) + "\" enumb=\"" + "0" + "\">@";
                            }
                            //MessageBox.Show(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(code_event_name[n].Remove(0, 2)));
                        }

                        for (var i = 0; i < code_event_action_index; i++)
                        {

                            rewrite += "            <action>@";

                            rewrite += "                <libid>" + "" + "</libid>@";
                            rewrite += "                <id>" + "" + "</id>@";
                            rewrite += "                <kind>" + "" + "</kind>@";
                            rewrite += "                <userelative>" + "" + "</userelative>@";
                            rewrite += "                <isquestion>" + "" + "</isquestion>@";
                            rewrite += "                <useapplyto>" + "" + "</useapplyto>@";
                            rewrite += "                <exetype>" + "" + "</exetype>@";
                            rewrite += "                <functionname>" + "" + "</functionname>@";
                            rewrite += "                <codestring>" + "" + "</codestring>@";
                            rewrite += "                <whoName>" + "" + "</whoName>@";
                            rewrite += "                <relative>" + "" + "</relative>@";
                            rewrite += "                <isnot>" + "" + "</isnot>@";
                            rewrite += "                <arguments>@";

                            rewrite += "                    <argument>@";

                            rewrite += "                        <kind>" + "" + "</kind>@";
                            rewrite += "                        <string>" + "" + "</string>@";

                            rewrite += "                    </argument>@";

                            rewrite += "                </arguments>@";
                            rewrite += "            </action>@";
                        }

                        rewrite += "        </event>@";
                    }

                    rewrite += "    </events>@";
                }
                else
                {
                    rewrite += "    <events/>@";
                }
                rewrite += "    <PhysicsObject>" + "0" + "</PhysicsObject>@";
                rewrite += "    <PhysicsObjectSensor>" + "0" + "</PhysicsObjectSensor>@";
                rewrite += "    <PhysicsObjectShape>" + "0" + "</PhysicsObjectShape>@";
                rewrite += "    <PhysicsObjectDensity>" + "0.5" + "</PhysicsObjectDensity>@";
                rewrite += "    <PhysicsObjectRestitution>" + "0.1" + "</PhysicsObjectRestitution>@";
                rewrite += "    <PhysicsObjectGroup>" + "0" + "</PhysicsObjectGroup>@";
                rewrite += "    <PhysicsObjectLinearDamping>" + "0.1" + "</PhysicsObjectLinearDamping>@";
                rewrite += "    <PhysicsObjectAngularDamping>" + "0.1" + "</PhysicsObjectAngularDamping>@";
                rewrite += "    <PhysicsObjectFriction>" + "0.2" + "</PhysicsObjectFriction>@";
                rewrite += "    <PhysicsObjectAwake>" + "-1" + "</PhysicsObjectAwake>@";
                rewrite += "    <PhysicsObjectKinematic>" + "0" + "</PhysicsObjectKinematic>@";
                rewrite += "    <PhysicsShapePoints/>@";
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

        public static int Event_To_Number(string Event)
        {
            var Number = -1;

            if (Event.IndexOf("[") != -1)
            {
                if (CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Event.Remove(0, 2)).Substring(0, Event.IndexOf("[") - (Event.IndexOf("]") - Event.IndexOf("["))) == "_Create")
                {
                    Number = 0;
                }
                else if (CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Event.Remove(0, 2)).Substring(0, Event.IndexOf("[") - (Event.IndexOf("]") - Event.IndexOf("["))) == "_Destroy")
                {
                    Number = 1;
                }
                else if (CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Event.Remove(0, 2)).Substring(0, Event.IndexOf("[") - (Event.IndexOf("]") - Event.IndexOf("["))) == "_Alarm")
                {
                    Number = 2;
                }
                else if (CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Event.Remove(0, 2)).Substring(0, Event.IndexOf("[") - (Event.IndexOf("]") - Event.IndexOf("["))) == "_Step_normal")
                {
                    Number = 3;
                }
                else if (CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Event.Remove(0, 2)).Substring(0, Event.IndexOf("[") - (Event.IndexOf("]") - Event.IndexOf("["))) == "_Step")
                {
                    Number = 3;
                }
                else if (CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Event.Remove(0, 2)).Substring(0, Event.IndexOf("[") - (Event.IndexOf("]") - Event.IndexOf("["))) == "_Step_begin")
                {
                    Number = 3;
                }
                else if (CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Event.Remove(0, 2)).Substring(0, Event.IndexOf("[") - (Event.IndexOf("]") - Event.IndexOf("["))) == "_Step_end")
                {
                    Number = 3;
                }
                else if (CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Event.Remove(0, 2)).Substring(0, Event.IndexOf("[") - (Event.IndexOf("]") - Event.IndexOf("["))) == "_Collisi")
                {
                    Number = 4;
                }
                else if (CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Event.Remove(0, 2)).Substring(0, Event.IndexOf("[") - (Event.IndexOf("]") - Event.IndexOf("["))) == "_Keyboard")
                {
                    Number = 5;
                }
                else if (CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Event.Remove(0, 2)).Substring(0, Event.IndexOf("[") - (Event.IndexOf("]") - Event.IndexOf("["))) == "_Mouse")
                {
                    Number = 6;
                }
                else if (CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Event.Remove(0, 2)).Substring(0, Event.IndexOf("[") - (Event.IndexOf("]") - Event.IndexOf("["))) == "_Other")
                {
                    Number = 7;
                }
                else if (CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Event.Remove(0, 2)).Substring(0, Event.IndexOf("[") - (Event.IndexOf("]") - Event.IndexOf("["))) == "_Outside")
                {
                    Number = 7;
                }
                else if (CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Event.Remove(0, 2)).Substring(0, Event.IndexOf("[") - (Event.IndexOf("]") - Event.IndexOf("["))) == "_Draw")
                {
                    Number = 8;
                }

                else if (CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Event.Remove(0, 2)).Substring(0, Event.IndexOf("[") - (Event.IndexOf("]") - Event.IndexOf("["))) == "_Keypress")
                {
                    Number = 9;
                }
                else if (CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Event.Remove(0, 2)).Substring(0, Event.IndexOf("[") - (Event.IndexOf("]") - Event.IndexOf("["))) == "_Keyrelease")
                {
                    Number = 10;
                }
                //MessageBox.Show(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Event.Remove(0, 2)).Substring(0, Event.IndexOf("[") - (Event.IndexOf("]") - Event.IndexOf("["))));
            }
            else
            {
                if (CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Event.Remove(0, 2)) == "_Create")
                {
                    Number = 0;
                }
                else if (CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Event.Remove(0, 2)) == "_Destroy")
                {
                    Number = 1;
                }
                else if (CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Event.Remove(0, 2)) == "_Alarm")
                {
                    Number = 2;
                }
                else if (CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Event.Remove(0, 2)) == "_Step_Normal")
                {
                    Number = 3;
                }
                else if (CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Event.Remove(0, 2)) == "_Step")
                {
                    Number = 3;
                }
                else if (CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Event.Remove(0, 2)) == "_Step_Begin")
                {
                    Number = 3;
                }
                else if (CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Event.Remove(0, 2)) == "_Step_End")
                {
                    Number = 3;
                }
                else if (CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Event.Remove(0, 2)) == "_Collision")
                {
                    Number = 4;
                }
                else if (CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Event.Remove(0, 2)) == "_Keyboard")
                {
                    Number = 5;
                }
                else if (CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Event.Remove(0, 2)) == "_Mouse")
                {
                    Number = 6;
                }
                else if (CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Event.Remove(0, 2)) == "_Other")
                {
                    Number = 7;
                }
                for (var i = 0; i < 16; i++)
                {
                    if (CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Event.Remove(0, 2)) == "_User" + i)
                    {
                        Number = 7;
                    }
                }
                if (CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Event.Remove(0, 2)) == "_Outside")
                {
                    Number = 7;
                }
                else if (CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Event.Remove(0, 2)) == "_Draw")
                {
                    Number = 8;
                }
                else if (CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Event.Remove(0, 2)) == "_Keypress")
                {
                    Number = 9;
                }
                else if (CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Event.Remove(0, 2)) == "_Keyrelease")
                {
                    Number = 10;
                }
                //MessageBox.Show(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Event.Remove(0, 2)));
            }


            return Number;
        }

        public static string[] Get_Files(string path)
        {
            string[] files = Directory.GetFiles(path);

            return files;
        }
    }
}
