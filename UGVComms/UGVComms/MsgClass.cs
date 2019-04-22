﻿using MessagePack;

namespace UGVComms
{
    /* Classes are being used instead of structs because structs are considered value types
     * which means they are copied when they are passed around. 
     * 
     * If you change a copy, changes are only made to that specific copy, and not to the 
     * original nor to any other copies around in use that you may have wanted to change as well.
     * 
     * Manipulating a struct's properties as you would a class would require you pass the struct
     * by reference, which such an implementation is significantly more prone to hard-to-catch 
     * mistakes, as well as reduced code maintainability. 
     * 
     * Using structs for this implementation would prove to be less efficient and not worth
     * the effort as performance increases are negligible when comparing classes and structs.
     */
    [MessagePackObject]
    public class MsgClass
    {
        [Key("type")]
        public string Type;
        [Key("id")]
        public int Id;
        [Key("sid")]
        public int Sid = 200;
        [Key("tid")]
        public int Tid;
        [Key("time")]
        public double Time;
    }

    //////////////messages to be sent/////////////
    public class ConnectMsg : MsgClass
    {
        [Key("jobsAvailable")]
        public string[] JobsAvailable;
        public ConnectMsg()
        {
            Type = "connect";
        }
    }

    public class UpdateMsg : MsgClass
    {
        [Key("lat")]
        public double Lat;
        [Key("lng")]
        public double Lng;
        [Key("status")]
        public string Status;
        [Key("heading")]
        public double Heading;
        [Key("battery")]
        public double Battery;
        public UpdateMsg()
        {
            Type = "update";
        }
    }

    public class POIMsg : MsgClass
    {
        [Key("lat")]
        public string Lat;
        [Key("lng")]
        public string Lng;
        public POIMsg()
        {
            Type = "poi";
        }
    }

    public class CompleteMsg : MsgClass
    {
        public CompleteMsg()
        {
            Type = "complete";
        }
    }

    //////////////messages to be received/////////////

    public class ConnAckMsg : MsgClass
    {
        public ConnAckMsg()
        {
            Type = "connectionAck";
        }
    }

    public class StartMsg : MsgClass
    {
        [Key("jobType")]
        public string jobType;
        public StartMsg()
        {
            Type = "start";
        }
    }

    public class AddMissionMsg : MsgClass
    {
        [Key("missionInfo")]
        public MissionInfo MissionInfo; // either retrieveTarget or deliverTarget; same values required
        public AddMissionMsg()
        {
            Type = "addMission";
        }
    }

    public class MissionInfo
    {
        [Key("taskType")]
        public string TaskType;
        [Key("lat")]
        public double Lat;
        [Key("lng")]
        public double Lng;
    }

    public class PauseMsg : MsgClass
    {
        public PauseMsg()
        {
            Type = "pause";
        }
    }

    public class ResumeMsg : MsgClass
    {
        public ResumeMsg()
        {
            Type = "resume";
        }
    }

    public class StopMsg : MsgClass
    {
        public StopMsg()
        {
            Type = "stop";
        }
    }

    //////////////other messages//////////////

    public class AckMsg : MsgClass
    {
        [Key("ackId")]
        public int AckId;
        public AckMsg()
        {
            Type = "ack";
        }
    }

    public class BadMsg : MsgClass
    {
        [Key("error")]
        public string Error;
        public BadMsg()
        {
            Type = "badMessage";
        }
    }
}
