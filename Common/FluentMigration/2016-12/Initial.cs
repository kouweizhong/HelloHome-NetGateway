using System;
using FluentMigrator;

namespace HelloHome.Common.FluentMigration
{
	[HhMigration (2016, 12, 2, 18, 41)]
	public class Initial : ForwardOnlyMigration
	{
		public override void Up ()
		{
			Execute.Sql ("CREATE TABLE `Node` (\n  `nodeId` int(11) NOT NULL,\n  `RfAddress` smallint(6) NOT NULL,\n  `emonCmsNodeId` smallint(6) DEFAULT NULL,\n  `validConfig` tinyint(1) NOT NULL DEFAULT '0',\n  `name` varchar(50) COLLATE latin1_general_ci DEFAULT '0',\n  `lastStartupTime` datetime DEFAULT NULL,\n  `upTime` float NOT NULL DEFAULT '0',\n  `maxUpTime` float NOT NULL DEFAULT '0',\n  `lastMsgRxTime` datetime DEFAULT NULL,\n  `lastRssi` smallint(6) NOT NULL DEFAULT '0',\n  `version` varchar(10) COLLATE latin1_general_ci DEFAULT NULL,\n  `currentTemperature` float DEFAULT NULL,\n  `currentHumidity` float DEFAULT NULL,\n  `currentPressure` int(11) DEFAULT NULL,\n  `currentVIn` float DEFAULT NULL,\n  `currentSendErrorCount` int(11) NOT NULL DEFAULT '0',\n  `confHal1` smallint(6) NOT NULL DEFAULT '0',\n  `confHal2` smallint(6) NOT NULL DEFAULT '0',\n  `confDry` smallint(6) NOT NULL DEFAULT '0',\n  `confVinTrigger` smallint(6) NOT NULL DEFAULT '0',\n  `confVinMeasure` smallint(6) NOT NULL DEFAULT '0',\n  `confSiEnable` tinyint(1) NOT NULL DEFAULT '0',\n  `confBmpEnable` tinyint(1) NOT NULL DEFAULT '0',\n  PRIMARY KEY (`nodeId`),\n  UNIQUE KEY `UX_NodeId` (`RfAddress`)\n) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;");
			Execute.Sql ("CREATE TABLE `SubNode` (\n  `subNodeId` int(10) NOT NULL AUTO_INCREMENT,\n  `nodeId` int(11) NOT NULL,\n  `number` tinyint(10) NOT NULL,\n  `name` varchar(50) COLLATE latin1_general_ci DEFAULT NULL,\n  `pulseCount` int(11) NOT NULL DEFAULT '0',\n  PRIMARY KEY (`subNodeId`),\n  KEY `FK_SubNode_Node` (`nodeId`),\n  CONSTRAINT `FK_SubNode_Node` FOREIGN KEY (`nodeId`) REFERENCES `Node` (`nodeId`)\n) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;");
			Execute.Sql ("CREATE TABLE `EnvironmentData` (\n  `id` int(11) NOT NULL AUTO_INCREMENT,\n  `nodeId` int(11) NOT NULL,\n  `timestamp` datetime NOT NULL,\n  `temperature` float DEFAULT NULL,\n  `humidity` float DEFAULT NULL,\n  `pressure` int(11) DEFAULT NULL,\n  PRIMARY KEY (`id`),\n  KEY `FK_EnvironmentData_Node` (`nodeId`),\n  CONSTRAINT `FK_EnvironmentData_Node` FOREIGN KEY (`nodeId`) REFERENCES `Node` (`nodeId`)\n) ENGINE=InnoDB AUTO_INCREMENT=266508 DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;");
			Execute.Sql ("CREATE TABLE `NodeInfoData` (\n  `id` int(11) NOT NULL AUTO_INCREMENT,\n  `nodeId` int(11) NOT NULL,\n  `timestamp` datetime NOT NULL,\n  `vIn` float DEFAULT NULL,\n  `sendErrorCount` int(11) NOT NULL DEFAULT '0',\n  PRIMARY KEY (`id`),\n  KEY `FK_BatteryData_Node` (`nodeId`),\n  CONSTRAINT `FK_BatteryData_Node` FOREIGN KEY (`nodeId`) REFERENCES `Node` (`nodeId`)\n) ENGINE=InnoDB AUTO_INCREMENT=28278 DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;");
			Execute.Sql ("CREATE TABLE `PulseData` (\n  `id` int(11) NOT NULL AUTO_INCREMENT,\n  `subNodeId` int(11) NOT NULL,\n  `timestamp` datetime NOT NULL,\n  `newPulses` tinyint(4) NOT NULL,\n  `newValue` int(11) NOT NULL,\n  PRIMARY KEY (`id`),\n  KEY `FK_PulseData_SubNode` (`subNodeId`),\n  CONSTRAINT `FK_PulseData_SubNode` FOREIGN KEY (`subNodeId`) REFERENCES `SubNode` (`subNodeId`)\n) ENGINE=InnoDB AUTO_INCREMENT=157724 DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;");
		}
	}
}
