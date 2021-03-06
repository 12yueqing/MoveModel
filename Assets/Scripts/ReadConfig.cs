﻿using UnityEngine;
using System.Collections;
using IniParser;
using IniParser.Model;
using System.IO;
using System.Collections.Generic;
using System;

/* Author		: Runing
** Time			: 18.9.28
** Describtion	: 
*/

public class NodeData
{
    public string Name;
    public string ID;
    public string Info;
    public bool HasSale;
    public string Value;
}

public class ReadConfig
{
    private static string _nodeconfigFile = Application.streamingAssetsPath + "/NodeConfig.ini";
    
    public Dictionary<string, NodeData> nodeDataDic = new Dictionary<string, NodeData>();

    //public static ReadConfig instance { get; set; } = new ReadConfig();

    //public void Init() { }

    public ReadConfig()
    {
        //初始化INIParser
        FileIniDataParser parser = new FileIniDataParser();
        parser.Parser.Configuration.AllowDuplicateKeys = true;
        parser.Parser.Configuration.OverrideDuplicateKeys = true;
        parser.Parser.Configuration.AllowDuplicateSections = true;

        IniData iniData = parser.ReadFile(_nodeconfigFile);

        //遍历ini中所有section
        foreach (var item in iniData.Sections)
        {
            try
            {
                string sectionName = item.SectionName;
                string name = sectionName;
                string id = iniData[sectionName]["ID"];
                string info = iniData[sectionName]["Info"];
                string hasSale = iniData[sectionName]["HasSale"];
                string value = iniData[sectionName]["Value"];

                if (string.IsNullOrEmpty(name))
                {
                    Debug.LogError("ReadConfig.ctor() : 不存在Name字段  sectionName:" + sectionName);
                    continue;
                }

                if (string.IsNullOrEmpty(info))
                {
                    Debug.LogError("ReadConfig.ctor() : 不存在Info字段  sectionName:" + sectionName);
                    continue;
                }

                NodeData nodeData = new NodeData();
                nodeData.Name = name;
                nodeData.Info = info.Trim();
                nodeData.ID = id.Trim();
                nodeData.HasSale = (hasSale.Trim() == "1") ? true : false;
                nodeData.Value = value.Trim();

                if (!nodeDataDic.ContainsKey(name))
                {
                    nodeDataDic.Add(name, nodeData);
                }
            }
            catch (Exception e)
            {
                Debug.LogError("ReadConfig.ctor() : 异常 " + e.Message);
            }
        }
    }
}