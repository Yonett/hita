#ifndef smpl_mesh_mdl
#define smpl_mesh_mdl 1

#include <map>
#include <set>

#include "SmplSLAE.h"

using namespace std;

/*==========================================================================================================================================
Структура, описывающая физические свойства материала. Знает, физические параметры описываемого материала.
============================================================================================================================================*/
struct meshMaterial
{
	unsigned int m_number;												//номер материала

	double	m_Pr;														//число Прандтля
	double	m_Gr;														//число Грасгофа

	double	m_temperatureConductivity;									//температуропроводность
	double	m_heatConductivity;											//теплопроводность
	double	m_kinematicViscosity;										//кинематическая вязкость
	double	m_beta;														//коэффициент теплового расширения

	double	m_rPoisson;													//коэффициент Пуассона
	double	m_tec;														//коэффициент линейного расширения
	double	m_em;														//модуль Юнга

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
Узел простой сетки
============================================================================================================================================*/
class smplMeshNode
{
public:
	//конструктор
	smplMeshNode(long int number, valarray<double>& coord)				{	m_number = number;	m_coord = coord; m_parentFacet = 0;	}

	//установить номер
	void setNumber(long int number)										{	m_number = number;										}
	//установить координаты
	void setCoord(valarray<double>& coord)								{	m_coord = coord;										}
	//установить дополнительный номер
	void setAdditionalNumber(short int type, long int number)			{	m_add_numbers[type] = number;							}
	//установить дополнительные координаты
	void setAdditionalCoord(short int type, valarray<double>& coord)	{	m_add_coords[type] = coord;								}

	//взять номер
	long int getNumber()												{	return m_number;										}
	//взять координаты
	valarray<double>& getCoord()										{	return m_coord;											}
	//взять дополнительный номер
	long int getAdditionalNumber(short int type)						{	return m_add_numbers.find(type) == m_add_numbers.end() ? 0 : m_add_numbers[type];		}
	//взять дополнительные координаты
	valarray<double>& getAdditionalCoord(short int type)				{	return m_add_coords.find(type) == m_add_coords.end() ? m_coord : m_add_coords[type];	}

	//задать грань-создателя
	void setParentFacet(smplMeshFacet* facet)							{	m_parentFacet = facet;									}
	//взять грань-создателя
	smplMeshFacet* getParentFacet()										{	return m_parentFacet;									}

	friend ostream& operator << (ostream& out, const smplMeshNode& source)
	{
		out << source.m_number << "\t" << source.m_coord.size();
		for (unsigned int i = 0; i < source.m_coord.size(); ++i)
			out << "\t" << source.m_coord[i];
		return out;
	}

private:
	long int							m_number;		//номер узла
	valarray<double>					m_coord;		//массив координат

	map<short int, long int>			m_add_numbers;	//массив дополнительных номеров узла
	map<short int, valarray<double>>	m_add_coords;	//массив дополнительных координат узла

	smplMeshFacet*						m_parentFacet;	//грань создатель

	//запрещаем конструктор копирования
	smplMeshNode(const smplMeshNode& source)	{}
	//запрещаем оператор присваивания
	const smplMeshNode operator = (const smplMeshNode& source)	{}
};

/*==========================================================================================================================================
Грань элемента простой сетки
============================================================================================================================================*/
class smplMeshFacet
{
public:
	//конструктор
	smplMeshFacet()														{	}
	//конструктор
	smplMeshFacet(short int type, valarray<smplMeshNode*> nodes)		{	m_type = type;	m_nodes = nodes;	}
	//установить тип
	void setType(short int type)										{	m_type = type;		}
	//взять тип
	short int getType()													{	return m_type;		}
	//взять массив узлов
	valarray<smplMeshNode*>& getNodes()									{	return m_nodes;		}
	//взять дочерние грани
	valarray<smplMeshFacet*>& getChildren()								{	return m_children;	}
	//взять порожденные грани
	valarray<smplMeshFacet*>& getNewFacets()							{	return m_newFacets;	}
	//взять массив конечных элементов
	list<smplMeshElement*>& getElementsList()							{	return m_elements;	}

	//добавить связь с элементом
	void addElement(smplMeshElement* element)
	{
		for (list<smplMeshElement*>::const_iterator item = m_elements.begin(); item != m_elements.end(); ++item)
			if (*item == element)	return;

		m_elements.push_front(element);
		return;
	}

	//создать по своему подобию
	virtual smplMeshFacet* create(short int type, valarray<smplMeshNode*> nodes)
	{	return new smplMeshFacet(type, nodes);	}

	//собрать номера узлов
	void collectNodeNumbers(map<size_t, size_t>& nodes)
	{
		for (size_t i = 0; i < m_nodes.size(); ++i)
			nodes[m_nodes[i]->getNumber()] = m_nodes[i]->getNumber();

		for (size_t i = 0; i < m_children.size(); ++i)
			m_children[i]->collectNodeNumbers(nodes);

		return;
	}

	//собрать все грани
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

	//оператор вывода в консоль
	friend ostream& operator << (ostream& out, const smplMeshFacet& source)
	{
		out << source.m_type << "\t" << source.m_nodes.size();
		for (unsigned int i = 0; i < source.m_nodes.size(); ++i)
			out << "\t" << source.m_nodes[i]->getNumber();
		return out;
	}

	//деструктор
	virtual ~smplMeshFacet()
	{
		for (size_t i = 0; i < m_children.size(); ++i)
			if (m_children[i] != 0)	delete m_children[i];
		for (size_t i = 0; i < m_newFacets.size(); ++i)
			if (m_newFacets[i] != 0)	delete m_newFacets[i];		
	}

protected:
	list<smplMeshElement*>				m_elements;		//список элементов ребра
	valarray<smplMeshNode*>				m_nodes;		//массив узлов грани элемента
	short int							m_type;			//тип грани элемента

	valarray<smplMeshFacet*>			m_children;		//дочернии грани
	valarray<smplMeshFacet*>			m_newFacets;	//порожденные грани

	//запрещаем конструктор копирования
	smplMeshFacet(const smplMeshFacet& source)	{}
	//запрещаем оператор присваивания
	const smplMeshFacet operator = (const smplMeshFacet& source)	{}
};

/*==========================================================================================================================================
Элемент простой сетки
============================================================================================================================================*/
class smplMeshElement
{
public:
	//пустой конструктор
	smplMeshElement()	{}

	//конструктор
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

	//установить материал
	void setMaterial(meshMaterial& material)								{	m_material = &material;						}
	//взять материал
	meshMaterial& getMaterial()												{	return (*m_material);						}

	//установить массив узлов
	void setNodes(valarray<smplMeshNode*>& nodes)							{	m_nodes = nodes;							}
	//взять массив узлов
	valarray<smplMeshNode*>& getNodes()										{	return m_nodes;								}

	//установить массив граней
	void setFacets(valarray<smplMeshFacet*>& facets)						{	m_facets = facets;							}
	//взять массив граней
	valarray<smplMeshFacet*>& getFacets()									{	return m_facets;							}

	//взять потомков
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

	//создать по своему подобию
	virtual smplMeshElement* create(meshMaterial& material, valarray<smplMeshFacet*>& facets)
	{	return new smplMeshElement(material, facets);	}

	//инициализация перед вычислениями
	virtual void init()		{}

	//добавить локальный вклад уравнения Температуры в глобальную матрицу СЛАУ
	virtual void addLocalEnergyMatrix(slae& matrix, valarray<double>& Vx, valarray<double>& Vy)		{}

	//добавить локальный вклад уравнения Вихря в глобальную матрицу СЛАУ
	virtual void addLocalVortexMatrix(slae& matrix, valarray<double>& Vx, valarray<double>& Vy, valarray<double>& heat) {}

	//добавить локальный вклад уравнения Векторного потенциала скорости в глобальную матрицу СЛАУ
	virtual void addLocalStreamMatrix(slae& matrix, valarray<double>& vortex) {}

	//добавить локальный вклад уравнения горизонтальной компоненты скорости в глобальную матрицу СЛАУ
	virtual void addLocalMatrix_Vx(slae& matrix, valarray<double>& stream) {}

	//добавить локальный вклад уравнения горизонтальной компоненты скорости в глобальную матрицу СЛАУ
	virtual void addLocalHorizontalVelocityMatrix(slae& matrix, valarray<double>& stream) {}

	//добавить локальный вклад уравнения вертикальной компоненты скорости в глобальную матрицу СЛАУ
	virtual void addLocalVerticalVelocityMatrix(slae& matrix, valarray<double>& stream) {}

	//добавить локальный вклад уравнения вихря для краевого условия в глобальную матрицу СЛАУ
	virtual void addLocalAltVortexMatrix(slae& matrix, valarray<double>& Vx, valarray<double>& Vy) {}

	//добавить локальный вклад временного члена в глобальную матрицу СЛАУ
	virtual void addLocalTransientMatrix(slae& matrix, double sigma, valarray<double>& lastStep) {}

	//деструктор
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
	valarray<smplMeshFacet*>			m_facets;		//массив граней элемента
	valarray<smplMeshNode*>				m_nodes;		//массив узлов элемента
	valarray<smplMeshElement*>			m_children;		//массив дочерних элементов
	meshMaterial*						m_material;		//материал элемента

	//запрещаем конструктор копирования
	smplMeshElement(const smplMeshElement& source)	{}
	//запрещаем оператор присваивания
	const smplMeshElement operator = (const smplMeshElement& source)	{}
};

/*==========================================================================================================================================
Простая сетка
============================================================================================================================================*/
class smplMesh
{
public:
	//конструктор
	smplMesh()	{}

	//добавить узел в сетку
	void addNode(valarray<double>& coord)							{	m_nodes.push_back(new smplMeshNode(m_nodes.size(), coord));	}
	//взять количество узлов
	long int getNodesCount()										{	return m_nodes.size();										}
	//создать индекс узлов
	void genNodeIndex();
	//взять узел по номеру
	smplMeshNode* getNodeByNumber(unsigned long int number);

	//добавить ребро в сетку
	bool addFacet(short int type, list<unsigned long int>& nodeNumbers, smplMeshFacet* sample, bool checkUniq = true);
	//создать индекс граней
	void genFacetIndex();
	//взять грань по номерам узлов
	smplMeshFacet* getFacetByNodeNumbers(list<unsigned long int>& nodeNumbers);

	//добавить материал
	void addMaterial(meshMaterial& source);

	//добавить элемент в сетку
	bool addElement(unsigned int material, valarray<list<unsigned long int> >& facets, smplMeshElement* sample);
	//создать индекс элементов
	void genElementIndex();
	//взять элемент по номерам узлов
	smplMeshElement* getElementByNodeNumbers(list<unsigned long int>& nodeNumbers);

	//сгенерировать матрицу для СЛАУ
	slae* genMatrix();
	//вернуть матрицу СЛАУ
	slae& getSLAE()		{	return m_slae;	}

	//в консоль
	void toStream(ostream& out);
	//вывод вектора значений в консоль
	void toStream(ostream& out, valarray<double>& source);

	//деструктор
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
	list<smplMeshElement*>	m_elements;										//элементы сетки
	list<smplMeshFacet*>	m_facets;										//грани сетки
	list<smplMeshNode*>		m_nodes;										//узлы сетки

	map<unsigned int, meshMaterial>				m_materials;				//материалы

	valarray<smplMeshNode*>						m_node_index;				//индекс для узлов сетки.
	valarray<list<smplMeshFacet*>>				m_facet_index;				//индекс для граней сетки (по первому узлу)
	map<unsigned int, list<smplMeshFacet*>>		m_facet_index_by_type;		//индекс для граней сетки (по типу)
	valarray<list<smplMeshElement*>>			m_element_index;			//индекс для элементов сетки (по первому узлу)

	slae	m_slae;															//матрица СЛАУ

	//запрещаем конструктор копирования
	smplMesh(const smplMesh& source)	{}
	//запрещаем оператор присваивания
	const smplMesh operator = (const smplMesh& source)	{}
};

//создать индекс узлов
void smplMesh::genNodeIndex()
{
	m_node_index.resize(m_nodes.size(), 0);
	for (list<smplMeshNode*>::const_iterator item = m_nodes.begin(); item != m_nodes.end(); ++item)
		m_node_index[(*item)->getNumber()] = *item;
	return;
}

//взять узел по номеру
smplMeshNode* smplMesh::getNodeByNumber(unsigned long int number)
{
	if (m_node_index.size() == m_nodes.size() && m_node_index.size() > number)	return m_node_index[number];

	for (list<smplMeshNode*>::const_iterator item = m_nodes.begin(); item != m_nodes.end(); ++item)
		if ((*item)->getNumber() == number)	return *item;

	return 0;
}

//добавить ребро в сетку
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

//создать индекс граней
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

//взять грань по номерам узлов
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

//добавить материал
void smplMesh::addMaterial(meshMaterial& source)
{
	m_materials[source.m_number] = source;
	return;
}

//добавить элемент в сетку
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

//создать индекс элементов
void smplMesh::genElementIndex()
{
	m_element_index.resize(m_nodes.size());
	for (list<smplMeshElement*>::const_iterator item = m_elements.begin(); item != m_elements.end(); ++item)
		m_element_index[(*item)->getNodes()[0]->getNumber()].push_front(*item);
	return;
}

//сгенерировать матрицу для СЛАУ
slae* smplMesh::genMatrix()
{
	valarray< set<size_t> >	connections;	//таблица связей
	valarray<size_t>		ig;				//массив указателей на строки
	valarray<size_t>		jg;				//массив положения в столбцах
	size_t i, j;

	//формируем таблицу связей
	connections.resize(m_nodes.size());

	list<smplMeshFacet*> facets;
	for (list<smplMeshFacet*>::const_iterator item = m_facets.begin(); item != m_facets.end(); ++item)
		(*item)->collectFacets(facets);

	for (list<smplMeshFacet*>::const_iterator item = facets.begin(); item != facets.end(); ++item)
		for (i = 0; i < (*item)->getNodes().size(); ++i)
			for (j = i + 1; j < (*item)->getNodes().size(); ++j)
				connections[max<size_t>((*item)->getNodes()[i]->getNumber(), (*item)->getNodes()[j]->getNumber())]
					.insert(min<size_t>((*item)->getNodes()[i]->getNumber(), (*item)->getNodes()[j]->getNumber()));

	//формируем портрет матрицы
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
	
	//отдаем матрицу
	m_slae.init(ig, jg);
	return &m_slae;
}

//в консоль
void smplMesh::toStream(ostream& out)
{
	//out << "\n" << m_nodes.size() << "\n";
	//for (list<smplMeshNode*>::const_iterator item = m_nodes.begin(); item != m_nodes.end(); ++item)
	//	out << *(*item) << "\n";

	//out << m_facets.size() << "\n";
	//for (list<smplMeshFacet*>::const_iterator item = m_facets.begin(); item != m_facets.end(); ++item)
	//	out << *(*item) << "\n";

	//out << m_materials.size() << "\n";
	//for(map<unsigned int, meshMaterial>::const_iterator item = m_materials.begin(); item != m_materials.end(); ++item)
	//	out << (*item).first << "\t" << (*item).second << "\n";

	//out << m_elements.size() << "\n";

	//for (list<smplMeshElement*>::const_iterator item = m_elements.begin(); item != m_elements.end(); ++item)
	//	out << *(*item) << "\n";

	for (list<smplMeshNode*>::const_iterator item = m_nodes.begin(); item != m_nodes.end(); ++item)
	{
		for (size_t i = 0; i < (*item)->getCoord().size(); ++i)
			out << (*item)->getCoord()[i] << "\t";
		out << "\n";
	}

	return;
}

//вывод вектора значений в консоль
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