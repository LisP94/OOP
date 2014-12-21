#pragma comment (lib, "System.lib")
#pragma comment (lib, "XmlUtils.lib")
#pragma comment (lib, "cppunit.lib")
#pragma comment (lib, "libexpat.lib")
#include "Grom\System\PropertyTree.h"
#include "Grom\XmlUtils\XmlUtils.h"
#include <string>

class XmlNode : public Sys::PropertyTree
{
private:
	void SetAttributes();
	void SetNodeText();

public:
	XmlNode();
	virtual ~XmlNode();
	void ReadFromFile(const char* path);
	void WriteToFile(const char* path);
	XmlNode* CreateChild(const char* name);
	void Convert();
};

class XmlAttribute : public XmlNode
{
public:
	XmlAttribute(XmlNode *attribute, const char* name, const char* value);
	virtual ~XmlAttribute();
};

class XmlText : public XmlNode
{
public:
	XmlText(XmlNode *node, const char* text);
	virtual ~XmlText();
};