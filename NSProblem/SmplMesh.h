#ifndef smpl_mesh_mdl
#define smpl_mesh_mdl 1

#include <map>
#include <set>

#include "SmplSLAE.h"

using namespace std;

/*==========================================================================================================================================
���������, ����������� ���������� �������� ���������. �����, ���������� ��������� ������������ ���������.
============================================================================================================================================*/
struct meshMaterial
{
	unsigned int m_number;												//����� ���������

	double	m_Pr;														//����� ��������
	double	m_Gr;														//����� ��������

	double	m_temperatureConductivity;									//����������������������
	double	m_heatConductivity;											//����������������
	double	m_kinematicViscosity;										//�������������� ��������
	double	m_beta;														//����������� ��������� ����������

	double	m_rPoisson;													//����������� ��������
	double	m_tec;														//����������� ��������� ����������
	double	m_em;														//������ ����

	double	m_factor;

	meshMaterial()
	{
		m_Pr = m_Gr = m_temperatureConductivity = m_kinematicViscosity = m_heatConductivity = m_beta = m_rPoisson = m_tec = m_em = 0;
		m_number = 0;
	}

	const meshMaterial& operator = (const meshMaterial& source)
	{
		m_number = source.m_number;

		m_Pr = source.m_Pr;
		m_Gr = source.m_Gr;

		m_temperatureConductivity	= source.m_temperatureConductivity;
		m_kinematicViscosity		= source.m_kinematicViscosity;
		m_heatConductivity			= source.m_heatConductivity;
		m_beta						= source.m_beta;

		m_rPoisson	= source.m_rPoisson;
		m_tec		= source.m_tec;
		m_em		= source.m_em;

		m_factor	= source.m_factor;

		return *this;
	}

	friend ostream& operator << (ostream& out, const meshMaterial& source)
	{
		out << source.m_number << "\t" << source.m_Pr << "\t" << source.m_Gr << "\t" << source.m_temperatureConductivity
			<< "\t" << source.m_kinematicViscosity << "\t" << source.m_heatConductivity << "\t" << source.m_beta
			<< "\t" << source.m_rPoisson << "\t" << source.m_tec << "\t" << source.m_em;
		return out;
	}
};

class smplMeshElement;
class smplMeshFacet;

/*==========================================================================================================================================
���� ������� �����
============================================================================================================================================*/
class smplMeshNode
{
public:
	//�����������
	smplMeshNode(long int number, valarray<double>& coord)				{	m_number = number;	m_coord = coord; m_parentFacet = 0;	}

	//���������� �����
	void setNumber(long int number)										{	m_number = number;										}
	//���������� ����������
	void setCoord(valarray<double>& coord)								{	m_coord = coord;										}
	//���������� �������������� �����
	void setAdditionalNumber(short int type, long int number)			{	m_add_numbers[type] = number;							}
	//���������� �������������� ����������
	void setAdditionalCoord(short int type, valarray<double>& coord)	{	m_add_coords[type] = coord;								}

	//����� �����
	long int getNumber()												{	return m_number;										}
	//����� ����������
	valarray<double>& getCoord()										{	return m_coord;											}
	//����� �������������� �����
	long int getAdditionalNumber(short int type)						{	return m_add_numbers.find(type) == m_add_numbers.end() ? 0 : m_add_numbers[type];		}
	//����� �������������� ����������
	valarray<double>& getAdditionalCoord(short int type)				{	return m_add_coords.find(type) == m_add_coords.end() ? m_coord : m_add_coords[type];	}

	//������ �����-���������
	void setParentFacet(smplMeshFacet* facet)							{	m_parentFacet = facet;									}
	//����� �����-���������
	smplMeshFacet* getParentFacet()										{	return m_parentFacet;									}

	friend ostream& operator << (ostream& out, const smplMeshNode& source)
	{
		out << source.m_number << "\t" << source.m_coord.size();
		for (unsigned int i = 0; i < source.m_coord.size(); ++i)
			out << "\t" << source.m_coord[i];
		return out;
	}

private:
	long int							m_number;		//����� ����
	valarray<double>					m_coord;		//������ ���������

	map<short int, long int>			m_add_numbers;	//������ �������������� ������� ����
	map<short int, valarray<double>>	m_add_coords;	//������ �������������� ��������� ����

	smplMeshFacet*						m_parentFacet;	//����� ���������

	//��������� ����������� �����������
	smplMeshNode(const smplMeshNode& source)	{}
	//��������� �������� ������������
	const smplMeshNode operator = (const smplMeshNode& source)	{}
};

/*==========================================================================================================================================
����� �������� ������� �����
============================================================================================================================================*/
class smplMeshFacet
{
public:
	//�����������
	smplMeshFacet()														{	}
	//�����������
	smplMeshFacet(short int type, valarray<smplMeshNode*> nodes)		{	m_type = type;	m_nodes = nodes;	}
	//���������� ���
	void setType(short int type)										{	m_type = type;		}
	//����� ���
	short int getType()													{	return m_type;		}
	//����� ������ �����
	valarray<smplMeshNode*>& getNodes()									{	return m_nodes;		}
	//����� �������� �����
	valarray<smplMeshFacet*>& getChildren()								{	return m_children;	}
	//����� ����������� �����
	valarray<smplMeshFacet*>& getNewFacets()							{	return m_newFacets;	}
	//����� ������ �������� ���������
	list<smplMeshElement*>& getElementsList()							{	return m_elements;	}

	//�������� ����� � ���������
	void addElement(smplMeshElement* element)
	{
		for (list<smplMeshElement*>::const_iterator item = m_elements.begin(); item != m_elements.end(); ++item)
			if (*item == element)	return;

		m_elements.push_front(element);
		return;
	}

	//������� �� ������ �������
	virtual smplMeshFacet* create(short int type, valarray<smplMeshNode*> nodes)
	{	return new smplMeshFacet(type, nodes);	}

	//������� ������ �����
	void collectNodeNumbers(map<size_t, size_t>& nodes)
	{
		for (size_t i = 0; i < m_nodes.size(); ++i)
			nodes[m_nodes[i]->getNumber()] = m_nodes[i]->getNumber();

		for (size_t i = 0; i < m_children.size(); ++i)
			m_children[i]->collectNodeNumbers(nodes);

		return;
	}

	//������� ��� �����
	void collectFacets(list<smplMeshFacet*>& facets)
	{
		if (m_children.size() == 0)		facets.push_back(this);

		for (size_t i = 0; i < m_children.size(); ++i)
			if (m_children[i] != 0)
				m_children[i]->collectFacets(facets);

		for (size_t i = 0; i < m_newFacets.size(); ++i)
			if (m_newFacets[i] != 0)
				m_newFacets[i]->collectFacets(facets);

		return;
	}

	//�������� ������ � �������
	friend ostream& operator << (ostream& out, const smplMeshFacet& source)
	{
		out << source.m_type << "\t" << source.m_nodes.size();
		for (unsigned int i = 0; i < source.m_nodes.size(); ++i)
			out << "\t" << source.m_nodes[i]->getNumber();
		return out;
	}

	//����������
	virtual ~smplMeshFacet()
	{
		for (size_t i = 0; i < m_children.size(); ++i)
			if (m_children[i] != 0)	delete m_children[i];
		for (size_t i = 0; i < m_newFacets.size(); ++i)
			if (m_newFacets[i] != 0)	delete m_newFacets[i];		
	}

protected:
	list<smplMeshElement*>				m_elements;		//������ ��������� �����
	valarray<smplMeshNode*>				m_nodes;		//������ ����� ����� ��������
	short int							m_type;			//��� ����� ��������

	valarray<smplMeshFacet*>			m_children;		//�������� �����
	valarray<smplMeshFacet*>			m_newFacets;	//����������� �����

	//��������� ����������� �����������
	smplMeshFacet(const smplMeshFacet& source)	{}
	//��������� �������� ������������
	const smplMeshFacet operator = (const smplMeshFacet& source)	{}
};

/*==========================================================================================================================================
������� ������� �����
============================================================================================================================================*/
class smplMeshElement
{
public:
	//������ �����������
	smplMeshElement()	{}

	//�����������
	smplMeshElement(meshMaterial& material, valarray<smplMeshFacet*>& facets)	
	{	
		m_material = &material;
		m_facets = facets;

		map<unsigned long int, smplMeshNode*> nodes;
		unsigned int j = 0;

		for (unsigned int i = 0; i < m_facets.size(); ++i)
		{
			m_facets[i]->addElement(this);

			for (j = 0; j < m_facets[i]->getNodes().size(); ++j)
				nodes[m_facets[i]->getNodes()[j]->getNumber()] = m_facets[i]->getNodes()[j];
		}

		m_nodes.resize(nodes.size(), 0); j = 0;
		for (map<unsigned long int, smplMeshNode*>::const_iterator item = nodes.begin(); item != nodes.end(); ++item, ++j)
			m_nodes[j] = (*item).second;
	}

	//���������� ��������
	void setMaterial(meshMaterial& material)								{	m_material = &material;						}
	//����� ��������
	meshMaterial& getMaterial()												{	return (*m_material);						}

	//���������� ������ �����
	void setNodes(valarray<smplMeshNode*>& nodes)							{	m_nodes = nodes;							}
	//����� ������ �����
	valarray<smplMeshNode*>& getNodes()										{	return m_nodes;								}

	//���������� ������ ������
	void setFacets(valarray<smplMeshFacet*>& facets)						{	m_facets = facets;							}
	//����� ������ ������
	valarray<smplMeshFacet*>& getFacets()									{	return m_facets;							}

	//����� ��������
	valarray<smplMeshElement*>& getChildren()								{	return m_children;							}

	friend ostream& operator << (ostream& out, const smplMeshElement& source)
	{
		out << source.m_material->m_number << "\t" << source.m_facets.size() << "\n";
		for (unsigned int i = 0; i < source.m_facets.size(); ++i)
			out << "\t\t" << *(source.m_facets[i]) << "\n";

		out << "\t" << source.m_children.size();
		for(size_t i = 0; i < source.m_children.size(); ++i)
			out << *(source.m_children[i]) << "\t";	

		return out;
	}

	//������� �� ������ �������
	virtual smplMeshElement* create(meshMaterial& material, valarray<smplMeshFacet*>& facets)
	{	return new smplMeshElement(material, facets);	}

	//������������� ����� ������������
	virtual void init()		{}

	//�������� ��������� ����� ��������� ����������� � ���������� ������� ����
	virtual void addLocalEnergyMatrix(slae& matrix, valarray<double>& Vx, valarray<double>& Vy)		{}

	//�������� ��������� ����� ��������� ����� � ���������� ������� ����
	virtual void addLocalVortexMatrix(slae& matrix, valarray<double>& Vx, valarray<double>& Vy, valarray<double>& heat) {}

	//�������� ��������� ����� ��������� ���������� ���������� �������� � ���������� ������� ����
	virtual void addLocalStreamMatrix(slae& matrix, valarray<double>& vortex) {}

	//�������� ��������� ����� ��������� �������������� ���������� �������� � ���������� ������� ����
	virtual void addLocalMatrix_Vx(slae& matrix, valarray<double>& stream) {}

	//�������� ��������� ����� ��������� �������������� ���������� �������� � ���������� ������� ����
	virtual void addLocalHorizontalVelocityMatrix(slae& matrix, valarray<double>& stream) {}

	//�������� ��������� ����� ��������� ������������ ���������� �������� � ���������� ������� ����
	virtual void addLocalVerticalVelocityMatrix(slae& matrix, valarray<double>& stream) {}

	//�������� ��������� ����� ��������� ����� ��� �������� ������� � ���������� ������� ����
	virtual void addLocalAltVortexMatrix(slae& matrix, valarray<double>& Vx, valarray<double>& Vy) {}

	//�������� ��������� ����� ���������� ����� � ���������� ������� ����
	virtual void addLocalTransientMatrix(slae& matrix, double sigma, valarray<double>& lastStep) {}

	//����������
	virtual ~smplMeshElement()
	{
		for (size_t i = 0; i < m_children.size(); ++i)
			if (m_children[i] != 0)	delete m_children[i];

		for (size_t i = 0; i < m_facets.size(); ++i)
			for (list<smplMeshElement*>::const_iterator item = m_facets[i]->getElementsList().begin(); item != m_facets[i]->getElementsList().end(); ++item)
				if ((*item) == this)
				{
					m_facets[i]->getElementsList().erase(item);
					break;
				}
	}

protected:
	valarray<smplMeshFacet*>			m_facets;		//������ ������ ��������
	valarray<smplMeshNode*>				m_nodes;		//������ ����� ��������
	valarray<smplMeshElement*>			m_children;		//������ �������� ���������
	meshMaterial*						m_material;		//�������� ��������

	//��������� ����������� �����������
	smplMeshElement(const smplMeshElement& source)	{}
	//��������� �������� ������������
	const smplMeshElement operator = (const smplMeshElement& source)	{}
};

/*==========================================================================================================================================
������� �����
============================================================================================================================================*/
class smplMesh
{
public:
	//�����������
	smplMesh()	{}

	//�������� ���� � �����
	void addNode(valarray<double>& coord)							{	m_nodes.push_back(new smplMeshNode(m_nodes.size(), coord));	}
	//����� ���������� �����
	long int getNodesCount()										{	return m_nodes.size();										}
	//������� ������ �����
	void genNodeIndex();
	//����� ���� �� ������
	smplMeshNode* getNodeByNumber(unsigned long int number);

	//�������� ����� � �����
	bool addFacet(short int type, list<unsigned long int>& nodeNumbers, smplMeshFacet* sample, bool checkUniq = true);
	//������� ������ ������
	void genFacetIndex();
	//����� ����� �� ������� �����
	smplMeshFacet* getFacetByNodeNumbers(list<unsigned long int>& nodeNumbers);

	//�������� ��������
	void addMaterial(meshMaterial& source);

	//�������� ������� � �����
	bool addElement(unsigned int material, valarray<list<unsigned long int> >& facets, smplMeshElement* sample);
	//������� ������ ���������
	void genElementIndex();
	//����� ������� �� ������� �����
	smplMeshElement* getElementByNodeNumbers(list<unsigned long int>& nodeNumbers);

	//������������� ������� ��� ����
	slae* genMatrix();
	//������� ������� ����
	slae& getSLAE()		{	return m_slae;	}

	//� �������
	void toStream(ostream& out);
	//����� ������� �������� � �������
	void toStream(ostream& out, valarray<double>& source);

	//����������
	virtual ~smplMesh()
	{
		for (list<smplMeshElement*>::const_iterator item = m_elements.begin(); item != m_elements.end(); ++item)
			delete (*item);
		for (list<smplMeshFacet*>::const_iterator item = m_facets.begin(); item != m_facets.end(); ++item)
			delete (*item);
		for (list<smplMeshNode*>::const_iterator item = m_nodes.begin(); item != m_nodes.end(); ++item)
			delete (*item);
	}

protected:
	list<smplMeshElement*>	m_elements;										//�������� �����
	list<smplMeshFacet*>	m_facets;										//����� �����
	list<smplMeshNode*>		m_nodes;										//���� �����

	map<unsigned int, meshMaterial>				m_materials;				//���������

	valarray<smplMeshNode*>						m_node_index;				//������ ��� ����� �����.
	valarray<list<smplMeshFacet*>>				m_facet_index;				//������ ��� ������ ����� (�� ������� ����)
	map<unsigned int, list<smplMeshFacet*>>		m_facet_index_by_type;		//������ ��� ������ ����� (�� ����)
	valarray<list<smplMeshElement*>>			m_element_index;			//������ ��� ��������� ����� (�� ������� ����)

	slae	m_slae;															//������� ����

	//��������� ����������� �����������
	smplMesh(const smplMesh& source)	{}
	//��������� �������� ������������
	const smplMesh operator = (const smplMesh& source)	{}
};

//������� ������ �����
void smplMesh::genNodeIndex()
{
	m_node_index.resize(m_nodes.size(), 0);
	for (list<smplMeshNode*>::const_iterator item = m_nodes.begin(); item != m_nodes.end(); ++item)
		m_node_index[(*item)->getNumber()] = *item;
	return;
}

//����� ���� �� ������
smplMeshNode* smplMesh::getNodeByNumber(unsigned long int number)
{
	if (m_node_index.size() == m_nodes.size() && m_node_index.size() > number)	return m_node_index[number];

	for (list<smplMeshNode*>::const_iterator item = m_nodes.begin(); item != m_nodes.end(); ++item)
		if ((*item)->getNumber() == number)	return *item;

	return 0;
}

//�������� ����� � �����
bool smplMesh::addFacet(short int type, list<unsigned long int>& nodeNumbers, smplMeshFacet* sample, bool checkUniq)
{
	if (checkUniq)
		if (getFacetByNodeNumbers(nodeNumbers) != 0)	return false;

	long int num = 0;
	valarray<smplMeshNode*> nodes;
	nodes.resize(nodeNumbers.size(), 0);
	
	for (list<unsigned long int>::const_iterator item = nodeNumbers.begin(); item != nodeNumbers.end(); ++item, ++num)
	{
		nodes[num] = getNodeByNumber(*item);
		if (nodes[num] == 0)	return true;
	}

	smplMeshFacet* facet = sample->create(type, nodes);
	m_facets.push_back(facet);
	return false;
}

//������� ������ ������
void smplMesh::genFacetIndex()
{
	m_facet_index.resize(m_nodes.size());
	m_facet_index_by_type.clear();

	for (list<smplMeshFacet*>::const_iterator item = m_facets.begin(); item != m_facets.end(); ++item)
	{
		m_facet_index_by_type[(*item)->getType()].push_front(*item);

		for (unsigned int i = 0; i < (*item)->getNodes().size(); ++i)
			m_facet_index[(*item)->getNodes()[i]->getNumber()].push_front(*item);
	}
	return;
}

//����� ����� �� ������� �����
smplMeshFacet* smplMesh::getFacetByNodeNumbers(list<unsigned long int>& nodeNumbers)
{
	bool flag;
	unsigned int i;
	list<smplMeshFacet*>* facetList;
	facetList = m_facet_index.size() == m_nodes.size() ? &m_facet_index[(*nodeNumbers.begin())] : &m_facets;
	
	for (list<smplMeshFacet*>::const_iterator facet = facetList->begin(); facet != facetList->end(); ++facet)
		if ((*facet)->getNodes().size() == nodeNumbers.size())
		{
			flag = true;

			for (list<unsigned long int>::const_iterator nodeNumber = nodeNumbers.begin(); nodeNumber != nodeNumbers.end() && flag; ++nodeNumber)
			{
				for (i = 0; i < (*facet)->getNodes().size(); ++i)
					if ((*facet)->getNodes()[i]->getNumber() == *(nodeNumber))
						break;

				flag = (i < (*facet)->getNodes().size());
			}

			if (flag)
				return *facet;
		}

	return 0;
}

//�������� ��������
void smplMesh::addMaterial(meshMaterial& source)
{
	m_materials[source.m_number] = source;
	return;
}

//�������� ������� � �����
bool smplMesh::addElement(unsigned int material, valarray<list<unsigned long int> >& facets, smplMeshElement* sample)
{
	valarray<smplMeshFacet*> Facets;
	Facets.resize(facets.size());

	for (unsigned int i = 0; i < facets.size(); ++i)
	{
		Facets[i] = getFacetByNodeNumbers(facets[i]);
		if (Facets[i] == 0)	return true;
	}

	smplMeshElement* element = sample->create(m_materials[material], Facets);
	element->init();
	m_elements.push_front(element);
	return false;
}

//������� ������ ���������
void smplMesh::genElementIndex()
{
	m_element_index.resize(m_nodes.size());
	for (list<smplMeshElement*>::const_iterator item = m_elements.begin(); item != m_elements.end(); ++item)
		m_element_index[(*item)->getNodes()[0]->getNumber()].push_front(*item);
	return;
}

//������������� ������� ��� ����
slae* smplMesh::genMatrix()
{
	valarray< set<size_t> >	connections;	//������� ������
	valarray<size_t>		ig;				//������ ���������� �� ������
	valarray<size_t>		jg;				//������ ��������� � ��������
	size_t i, j;

	//��������� ������� ������
	connections.resize(m_nodes.size());

	list<smplMeshFacet*> facets;
	for (list<smplMeshFacet*>::const_iterator item = m_facets.begin(); item != m_facets.end(); ++item)
		(*item)->collectFacets(facets);

	for (list<smplMeshFacet*>::const_iterator item = facets.begin(); item != facets.end(); ++item)
		for (i = 0; i < (*item)->getNodes().size(); ++i)
			for (j = i + 1; j < (*item)->getNodes().size(); ++j)
				connections[max<size_t>((*item)->getNodes()[i]->getNumber(), (*item)->getNodes()[j]->getNumber())]
					.insert(min<size_t>((*item)->getNodes()[i]->getNumber(), (*item)->getNodes()[j]->getNumber()));

	//��������� ������� �������
	for (j = connections[0].size(), i = 1; i < connections.size(); ++i)
		j += connections[i].size();

	ig.resize(connections.size() + 1);
	jg.resize(j);

	ig[0] = ig[1] = 0;

	set<size_t>::iterator pos;
	for (i = 1, j = 0; i < connections.size(); ++i)
	{
		for (pos = connections[i].begin(); pos != connections[i].end(); ++pos, ++j)
			jg[j] = *pos;

		ig[i + 1] = ig[i] + connections[i].size();
	}
	
	//������ �������
	m_slae.init(ig, jg);
	return &m_slae;
}

//� �������
void smplMesh::toStream(ostream& out)
{
	out << "\n" << m_nodes.size() << "\n";
	for (list<smplMeshNode*>::const_iterator item = m_nodes.begin(); item != m_nodes.end(); ++item)
		out << *(*item) << "\n";

	out << m_facets.size() << "\n";
	for (list<smplMeshFacet*>::const_iterator item = m_facets.begin(); item != m_facets.end(); ++item)
		out << *(*item) << "\n";

	out << m_materials.size() << "\n";
	for(map<unsigned int, meshMaterial>::const_iterator item = m_materials.begin(); item != m_materials.end(); ++item)
		out << (*item).first << "\t" << (*item).second << "\n";

	out << m_elements.size() << "\n";

	for (list<smplMeshElement*>::const_iterator item = m_elements.begin(); item != m_elements.end(); ++item)
		out << *(*item) << "\n";

	return;
}

//����� ������� �������� � �������
void smplMesh::toStream(ostream& out, valarray<double>& source)
{
	for (list<smplMeshNode*>::const_iterator item = m_nodes.begin(); item != m_nodes.end(); ++item)
	{
		for(size_t i = 0; i < (*item)->getCoord().size(); ++i)
			out << (*item)->getCoord()[i] << "\t";

		out << source[(*item)->getNumber()] << "\n";
	}

	return;
}

#endif