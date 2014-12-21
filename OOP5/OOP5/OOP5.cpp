// OOP5.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "XmlNode.h"

int _tmain(int argc, _TCHAR* argv[])
{
	XmlNode *xmlNode;
	__try
	{
		xmlNode = Sys::Create< XmlNode>(Sys::GlobalOwner());
		xmlNode->ReadFromFile("Input.xml");
		xmlNode->Convert();
		xmlNode->WriteToFile("Output.xml");
	}
	__finally
	{
		xmlNode->Destroy();
	}
	return 0;
}

