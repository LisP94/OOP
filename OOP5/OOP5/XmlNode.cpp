#include "stdafx.h"
#include "XmlNode.h"


//XmlNode

XmlNode::XmlNode() : Sys::PropertyTree() 
{

}

XmlNode::~XmlNode() 
{

}

void XmlNode::ReadFromFile(const char* path)
{
	Utl::LoadPropertyTreeFromXmlFile(this, path);
}

void XmlNode::WriteToFile(const char* path)
{
	Utl::SavePropertyTreeToXmlFile(this, path);
}

XmlNode* XmlNode::CreateChild(const char* name)
{
	if (this == NULL)
		return NULL;
	else
		return reinterpret_cast<XmlNode*>(Sys::PropertyTree::CreateChild(name));
}

void XmlNode::SetAttributes()
{
	if (ItemCount() > 0)
	{
		XmlNode *attribute = CreateChild("attributes"); 
		int itemCount = ItemCount();
		for (int i = 0; i < itemCount; i++)
		{
			new XmlAttribute(attribute, ItemName(0), ItemValue(0));
			RemoveItemAt(0);
		}
	}
}

void XmlNode::Convert()
{
	SetAttributes(); 
	SetNodeText();
	if (this != NULL)
	{
		for (int i = 0; i < this->ChildCount(); i++) 
		{
			XmlNode *node = reinterpret_cast<XmlNode*>(Child(i)); 
			node->SetAttributes();
			node->SetNodeText();
			node->Convert();
		}
	}
}

void XmlNode::SetNodeText()
{
	const char* text1 = Text();
	if (strcmp(Text(), "") && strcmp(Name(), "text") && ChildCount() > 0)
	{
		const char* text = Text();
		new XmlText(this, text);
	}
}

//XmlAttribute

XmlAttribute::XmlAttribute(XmlNode *attribute, const char* name, const char* value)
{
	XmlNode* item = attribute->CreateChild(name);
	item->SetText(value);
}

XmlAttribute::~XmlAttribute() 
{

}

//XmlText

XmlText::XmlText(XmlNode *node, const char* text)
{
	XmlNode *textNode = node->CreateChild("text");
	std::string instring(text), outstring("");
	for (int i = 0; i < instring.length(); i++)
	{
		if (instring[i] == '\t' || instring[i] == '\n')
		{
			instring[i] = ' ';
		}
		if (instring[i] == ' ')
		{
			if (i == 0 || outstring[outstring.length() - 1] == ' ')
				continue;
		}
		outstring += instring[i];
	}
	textNode->SetText(outstring.c_str());
	node->SetText("");
}

XmlText::~XmlText()
{

}