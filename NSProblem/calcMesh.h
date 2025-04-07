#ifndef calc_mesh_mdl
#define calc_mesh_mdl 1

#include "SmplMesh.h"
using namespace std;

/*==========================================================================================================================================
Элемент грани для треугольника
============================================================================================================================================*/
class meshFacet: public smplMeshFacet
{
public:
	//пустой конструктор
	meshFacet()	{}

	//конструктор
	meshFacet(short int type, valarray<smplMeshNode*> nodes) : smplMeshFacet(type, nodes)	{}

	//создать по своему подобию
	virtual smplMeshFacet* create(short int type, valarray<smplMeshNode*> nodes)
	{	return new meshFacet(type, nodes);	}

	//вычислить свой размер
	virtual double getSize()
	{	
		return sqrt(
				(m_nodes[0]->getCoord()[0] - m_nodes[1]->getCoord()[0]) * (m_nodes[0]->getCoord()[0] - m_nodes[1]->getCoord()[0]) +
				(m_nodes[0]->getCoord()[1] - m_nodes[1]->getCoord()[1]) * (m_nodes[0]->getCoord()[1] - m_nodes[1]->getCoord()[1])
					);	
	}
protected:
};

/*==========================================================================================================================================
Элемент сетки для жидкости в декартовых координатах
============================================================================================================================================*/
class meshElement: public smplMeshElement
{
public:
	//пустой конструктор
	meshElement()	{}

	//конструктор
	meshElement(meshMaterial& material, valarray<smplMeshFacet*>& facets) : smplMeshElement(material, facets)
	{	
	}

	//создать по своему подобию
	smplMeshElement* create(meshMaterial& material, valarray<smplMeshFacet*>& facets)
	{	return new meshElement(material, facets);	}

	//инициализация перед вычислениями
	void init()
	{
		double	XY[3][2];

		XY[0][0] = m_nodes[0]->getCoord()[0];
		XY[0][1] = m_nodes[0]->getCoord()[1];
		XY[1][0] = m_nodes[1]->getCoord()[0];
		XY[1][1] = m_nodes[1]->getCoord()[1];
		XY[2][0] = m_nodes[2]->getCoord()[0];
		XY[2][1] = m_nodes[2]->getCoord()[1];

		m_detD	= (XY[1][0] - XY[0][0])*(XY[2][1] - XY[0][1]) - (XY[2][0] - XY[0][0])*(XY[1][1] - XY[0][1]);

		m_A[0][0]	= (XY[1][0]*XY[2][1] - XY[2][0]*XY[1][1])/m_detD;
		m_A[1][0]	= (XY[2][0]*XY[0][1] - XY[0][0]*XY[2][1])/m_detD;
		m_A[2][0]	= (XY[0][0]*XY[1][1] - XY[0][0]*XY[1][1])/m_detD;

		m_A[0][1]	= (XY[1][1] - XY[2][1])/m_detD;
		m_A[1][1]	= (XY[2][1] - XY[0][1])/m_detD;
		m_A[2][1]	= (XY[0][1] - XY[1][1])/m_detD;

		m_A[0][2]	= (XY[2][0] - XY[1][0])/m_detD;
		m_A[1][2]	= (XY[0][0] - XY[2][0])/m_detD;
		m_A[2][2]	= (XY[1][0] - XY[0][0])/m_detD;

		m_detD = fabs(m_detD);

		if (m_children.size() > 0)
		{	m_children[0]->init();	m_children[1]->init();	}

		return;
	}

	//добавить локальный вклад уравнения Температуры в глобальную матрицу СЛАУ
	void addLocalEnergyMatrix(slae& matrix, valarray<double>& Vx, valarray<double>& Vy)
	{
		if (m_children.size() > 0)
		{	
			m_children[0]->addLocalEnergyMatrix(matrix, Vx, Vy);
			m_children[1]->addLocalEnergyMatrix(matrix, Vx, Vy);
			return;
		}

		short int i, j;
		double val;

		for (i = 0; i < 3; ++i)
		for (j = 0; j < 3; ++j)
		{
			val	=	m_material->m_heatConductivity * 0.5*(m_A[i][1]*m_A[j][1] + m_A[i][2]*m_A[j][2])*m_detD;

			val	+=	(Vx[m_nodes[0]->getNumber()] + Vx[m_nodes[1]->getNumber()] + Vx[m_nodes[2]->getNumber()] +
					 Vx[m_nodes[i]->getNumber()])*m_A[j][1]*m_detD/24.;

			val	+=	(Vy[m_nodes[0]->getNumber()] + Vy[m_nodes[1]->getNumber()] + Vy[m_nodes[2]->getNumber()] +
					 Vy[m_nodes[i]->getNumber()])*m_A[j][2]*m_detD/24.;

			matrix.addValue(m_nodes[i]->getNumber(), m_nodes[j]->getNumber(), -val);
		}

		return;
	}

	//добавить локальный вклад уравнения Вихря в глобальную матрицу СЛАУ
	void addLocalVortexMatrix(slae& matrix, valarray<double>& Vx, valarray<double>& Vy, valarray<double>& heat)
	{
		if (m_children.size() > 0)
		{	
			m_children[0]->addLocalVortexMatrix(matrix, Vx, Vy, heat);
			m_children[1]->addLocalVortexMatrix(matrix, Vx, Vy, heat);
			return;
		}

		short int i, j;
		double val;

		for (i = 0; i < 3; ++i)
		for (j = 0; j < 3; ++j)
		{
			val	=	m_material->m_kinematicViscosity * 0.5*m_material->m_Pr*(m_A[i][1]*m_A[j][1] + m_A[i][2]*m_A[j][2])*m_detD;

			val	+=	(Vx[m_nodes[0]->getNumber()] + Vx[m_nodes[1]->getNumber()] + Vx[m_nodes[2]->getNumber()] +
						Vx[m_nodes[i]->getNumber()])*m_A[j][1]*m_detD/24.;

			val	+=	(Vy[m_nodes[0]->getNumber()] + Vy[m_nodes[1]->getNumber()] + Vy[m_nodes[2]->getNumber()] +
						Vy[m_nodes[i]->getNumber()])*m_A[j][2]*m_detD/24.;

			matrix.addValue(m_nodes[i]->getNumber(), m_nodes[j]->getNumber(), val);
			matrix[m_nodes[i]->getNumber()] -= m_material->m_beta * m_material->m_Gr * m_material->m_Pr * m_material->m_Pr * heat[m_nodes[j]->getNumber()]*m_A[j][1] * m_detD/6.;
		}

		return;
	}

	//добавить локальный вклад уравнения Векторного потенциала скорости в глобальную матрицу СЛАУ
	void addLocalStreamMatrix(slae& matrix, valarray<double>& vortex)
	{
		if (m_children.size() > 0)
		{	
			m_children[0]->addLocalStreamMatrix(matrix, vortex);
			m_children[1]->addLocalStreamMatrix(matrix, vortex);
			return;
		}

		short int i, j;

		for (i = 0; i < 3; ++i)
		{
			for (j = 0; j < 3; ++j)
				matrix.addValue(m_nodes[i]->getNumber(), m_nodes[j]->getNumber(), -0.5*(m_A[i][1]*m_A[j][1] + m_A[i][2]*m_A[j][2])*m_detD);
			matrix[m_nodes[i]->getNumber()] += (vortex[m_nodes[0]->getNumber()] + vortex[m_nodes[1]->getNumber()] + vortex[m_nodes[2]->getNumber()] + vortex[m_nodes[i]->getNumber()])*m_detD/24.;
		}

		return;
	}

	//добавить локальный вклад уравнения горизонтальной компоненты скорости в глобальную матрицу СЛАУ
	void addLocalHorizontalVelocityMatrix(slae& matrix, valarray<double>& stream)
	{
		if (m_children.size() > 0)
		{	
			m_children[0]->addLocalHorizontalVelocityMatrix(matrix, stream);
			m_children[1]->addLocalHorizontalVelocityMatrix(matrix, stream);
			return;
		}

		short int i;

		for (i = 0; i < 3; ++i)
		{
			matrix(m_nodes[i]->getNumber(), m_nodes[i]->getNumber()) += 1.0/m_detD;
			matrix[m_nodes[i]->getNumber()]	+= (stream[m_nodes[0]->getNumber()]*m_A[0][2] + stream[m_nodes[1]->getNumber()]*m_A[1][2] + stream[m_nodes[2]->getNumber()]*m_A[2][2])/m_detD;
		}

		return;
	}

	//добавить локальный вклад уравнения вертикальной компоненты скорости в глобальную матрицу СЛАУ
	void addLocalVerticalVelocityMatrix(slae& matrix, valarray<double>& stream)
	{
		if (m_children.size() > 0)
		{	
			m_children[0]->addLocalVerticalVelocityMatrix(matrix, stream);
			m_children[1]->addLocalVerticalVelocityMatrix(matrix, stream);
			return;
		}

		short int i;

		for (i = 0; i < 3; ++i)
		{
			matrix(m_nodes[i]->getNumber(), m_nodes[i]->getNumber()) += 1.0/m_detD;
			matrix[m_nodes[i]->getNumber()]	-= (stream[m_nodes[0]->getNumber()]*m_A[0][1] + stream[m_nodes[1]->getNumber()]*m_A[1][1] + stream[m_nodes[2]->getNumber()]*m_A[2][1])/m_detD;
		}

		return;
	}

	//добавить локальный вклад уравнения вихря для краевого условия в глобальную матрицу СЛАУ
	void addLocalAltVortexMatrix(slae& matrix, valarray<double>& Vx, valarray<double>& Vy)
	{
		if (m_children.size() > 0)
		{	
			m_children[0]->addLocalAltVortexMatrix(matrix, Vx, Vy);
			m_children[1]->addLocalAltVortexMatrix(matrix, Vx, Vy);
			return;
		}

		short int i;

		for (i = 0; i < 3; ++i)
		{
			matrix(m_nodes[i]->getNumber(), m_nodes[i]->getNumber()) += 1.0/m_detD;
			matrix[m_nodes[i]->getNumber()]	-= (Vy[m_nodes[0]->getNumber()]*m_A[0][1] + Vy[m_nodes[1]->getNumber()]*m_A[1][1] + Vy[m_nodes[2]->getNumber()]*m_A[2][1])/m_detD;
			matrix[m_nodes[i]->getNumber()]	+= (Vx[m_nodes[0]->getNumber()]*m_A[0][2] + Vx[m_nodes[1]->getNumber()]*m_A[1][2] + Vx[m_nodes[2]->getNumber()]*m_A[2][2])/m_detD;
		}

		return;
	}

	//добавить локальный вклад временного члена в глобальную матрицу СЛАУ
	void addLocalTransientMatrix(slae& matrix, double sigma, valarray<double>& lastStep)
	{
		if (m_children.size() > 0)
		{	
			m_children[0]->addLocalTransientMatrix(matrix, sigma, lastStep);
			m_children[1]->addLocalTransientMatrix(matrix, sigma, lastStep);
			return;
		}

		short int i, j;
		double val;

		for (i = 0; i < 3; ++i)
		for (j = 0; j < 3; ++j)
		{
			val = (1 + (i == j)) * sigma * m_detD / 24.;

			matrix(m_nodes[i]->getNumber(), m_nodes[j]->getNumber()) +=	val;
			matrix[m_nodes[i]->getNumber()]	+= val*lastStep[m_nodes[j]->getNumber()];
		}

		return;
	}

private:
	double			m_A[3][3];			//вспомогательный массив
	double			m_detD;				//определитель

	smplMeshNode*	m_divisionNode;		//разделяющий узел
};
/*==========================================================================================================================================
Cетка для квадратной каверны в декартовых координатах
============================================================================================================================================*/
class calcMesh : public smplMesh
{
public:
	//сгенерировать сетку
	void genMesh(double Gr, double Pr, double width, double height, long int sptCnt_w, long int sptCnt_h, bool heatedSide);
	//сгенерировать индексы узлов для КУ
	void genBCNodes();

	//сформировать СЛАУ для Температуры
	void genEnergySLAE(valarray<double>& Vx, valarray<double>& Vy);
	//сформировать СЛАУ для Температуры (нестационарный)
	void genEnergySLAE(valarray<double>& Vx, valarray<double>& Vy, double sigma, valarray<double>& lastStep);
	//сформировать СЛАУ для Вихря
	void genVortexSLAE(valarray<double>& Vx, valarray<double>& Vy, valarray<double>& T, valarray<double>& aW);
	//сформировать СЛАУ для Вихря (нестационарный)
	void genVortexSLAE(valarray<double>& Vx, valarray<double>& Vy, valarray<double>& T, valarray<double>& aW, double sigma, valarray<double>& lastStep);
	//сформировать СЛАУ для Функции тока
	void genStreamSLAE(valarray<double>& Vortex);
	//сформировать СЛАУ для горизонтальной скорости
	void genHorizontalVelocitySLAE(valarray<double>& Stream);
	//сформировать СЛАУ для вертикальной скорости
	void genVerticalVelocitySLAE(valarray<double>& Stream);
	//сформировать СЛАУ для КУ вихря
	void genAltVortexSLAE(valarray<double>& Vx, valarray<double>& Vy);

protected:
	map<long int, double> m_bc_t_nodeList;					//узлы для КУ температуры
	map<long int, double> m_bc_rs_nodeList;					//узлы для жесткой поверхности
	map<long int, double> m_bc_fs_nodeList;					//узлы для свободной поверхности
	map<long int, double> m_bc_fss_nodeList;				//узлы над свободной поверхностью
};

//сгенерировать сетку
void calcMesh::genMesh(double Gr, double Pr, double width, double height, long int sptCnt_w, long int sptCnt_h, bool heatedSide)
{
	valarray<double>	coord(2);
	meshMaterial		material;
	
	material.m_Gr = Gr;
	material.m_Pr = Pr;

	material.m_kinematicViscosity = 1.0;
	material.m_heatConductivity = 1.0;
	material.m_beta = 1.0;

	material.m_number = 0;
	addMaterial(material);

	double step_w = width/sptCnt_w;
	double step_h = height/sptCnt_h;
	long int i, j;

	for (j = 0; j <= sptCnt_h; ++j)
	for (i = 0; i <= sptCnt_w; ++i)
	{
		coord[0] = i*step_w;
		coord[1] = j*step_h;
		addNode(coord);
	}

	genNodeIndex();
	list<unsigned long int> nodes;
	smplMeshFacet* sampleFacet = new meshFacet();

	for (j = 0; j <= sptCnt_h; ++j)
	for (i = 0; i <= sptCnt_w; ++i)
	{
		//0 - горячая стенка
		//1 - холодная стенка
		//2 - теплоизолированный торец вверху
		//3 - теплоизолированный торец внизу
		//4 - простое ребро
		//5 - свободная поверхность

		if (i != sptCnt_w)
		{
			nodes.clear();
			nodes.push_back(i + j*(sptCnt_w + 1));
			nodes.push_back(i + j*(sptCnt_w + 1) + 1);
			addFacet(j == 0 ? (heatedSide ? 3 : 0) : j == sptCnt_h ? (heatedSide ? 2 : 1) : 4, nodes, sampleFacet, false);
		}
		if (j != sptCnt_h)
		{
			nodes.clear();
			nodes.push_back(i + j*(sptCnt_w + 1));
			nodes.push_back(i + (j + 1)*(sptCnt_w + 1));
			addFacet(i == 0 ? (heatedSide ? 0 : 2) : i == sptCnt_w ? (heatedSide ? 1 : 3) : 4, nodes, sampleFacet, false);
		}
		if ((i + j)%2 == 0 && j != sptCnt_h)
		{
			if (i != sptCnt_w)
			{
				nodes.clear();
				nodes.push_back(i + j*(sptCnt_w + 1));
				nodes.push_back(i + (j + 1)*(sptCnt_w + 1) + 1);
				addFacet(4, nodes, sampleFacet, false);
			}
			if (i != 0)
			{
				nodes.clear();
				nodes.push_back(i + j*(sptCnt_w + 1));
				nodes.push_back(i + (j + 1)*(sptCnt_w + 1) - 1);
				addFacet(4, nodes, sampleFacet, false);
			}
		}
	}

	genFacetIndex();

	valarray<list<unsigned long int> > facets(3);
	smplMeshElement* sample = new meshElement();

	for (j = 0; j < sptCnt_h; ++j)
	for (i = 0; i < sptCnt_w; ++i)
	{
		facets[0].clear();
		facets[1].clear();
		facets[2].clear();

		facets[0].push_front(i + j*(sptCnt_w + 1));
		facets[0].push_front(i + j*(sptCnt_w + 1) + 1);

		facets[1].push_front(i + j*(sptCnt_w + 1) + (1 - (i + j)%2));
		facets[1].push_front(i + (j + 1)*(sptCnt_w + 1) + (1 - (i + j)%2));

		facets[2].push_front(i + j*(sptCnt_w + 1) + (i + j)%2);
		facets[2].push_front(i + (j + 1)*(sptCnt_w + 1) + (1 - (i + j)%2));

		addElement(0, facets, sample);

		facets[0].clear();
		facets[1].clear();
		facets[2].clear();

		facets[0].push_front(i + (j + 1)*(sptCnt_w + 1));
		facets[0].push_front(i + (j + 1)*(sptCnt_w + 1) + 1);

		facets[1].push_front(i + j*(sptCnt_w + 1) + (i + j)%2);
		facets[1].push_front(i + (j + 1)*(sptCnt_w + 1) + (i + j)%2);

		facets[2].push_front(i + j*(sptCnt_w + 1) + (i + j)%2);
		facets[2].push_front(i + (j + 1)*(sptCnt_w + 1) + (1 - (i + j)%2));

		addElement(0, facets, sample);
	}

	genElementIndex();
	genBCNodes();

	return;
}

//сгенерировать индексы узлов для КУ
void calcMesh::genBCNodes()
{
	m_bc_t_nodeList.clear();
	m_bc_rs_nodeList.clear();

	map<size_t, size_t> nodes;
	for (list<smplMeshFacet*>::const_iterator item = m_facet_index_by_type[0].begin(); item != m_facet_index_by_type[0].end(); ++item)
		(*item)->collectNodeNumbers(nodes);

	for (map<size_t, size_t>::const_iterator item = nodes.begin(); item != nodes.end(); ++item)
		m_bc_t_nodeList[(*item).first] = 1.0;

	nodes.clear();
	for (list<smplMeshFacet*>::const_iterator item = m_facet_index_by_type[1].begin(); item != m_facet_index_by_type[1].end(); ++item)
		(*item)->collectNodeNumbers(nodes);

	for (map<size_t, size_t>::const_iterator item = nodes.begin(); item != nodes.end(); ++item)
		m_bc_t_nodeList[(*item).first] = 0.0;

	for (list<smplMeshFacet*>::const_iterator item = m_facet_index_by_type[0].begin(); item != m_facet_index_by_type[0].end(); ++item)
		(*item)->collectNodeNumbers(nodes);
	for (list<smplMeshFacet*>::const_iterator item = m_facet_index_by_type[2].begin(); item != m_facet_index_by_type[2].end(); ++item)
		(*item)->collectNodeNumbers(nodes);
	for (list<smplMeshFacet*>::const_iterator item = m_facet_index_by_type[3].begin(); item != m_facet_index_by_type[3].end(); ++item)
		(*item)->collectNodeNumbers(nodes);

	for (map<size_t, size_t>::const_iterator item = nodes.begin(); item != nodes.end(); ++item)
		m_bc_rs_nodeList[(*item).first] = 0.0;

	nodes.clear();
	for (list<smplMeshFacet*>::const_iterator item = m_facet_index_by_type[5].begin(); item != m_facet_index_by_type[5].end(); ++item)
		(*item)->collectNodeNumbers(nodes);

	for (map<size_t, size_t>::const_iterator item = nodes.begin(); item != nodes.end(); ++item)
		m_bc_fs_nodeList[(*item).first] = 0.0;

	nodes.clear();
	for (list<smplMeshFacet*>::const_iterator item = m_facet_index_by_type[6].begin(); item != m_facet_index_by_type[6].end(); ++item)
		(*item)->collectNodeNumbers(nodes);

	for (map<size_t, size_t>::const_iterator item = nodes.begin(); item != nodes.end(); ++item)
		m_bc_fss_nodeList[(*item).first] = 0.0;

	return;
}

//сформировать СЛАУ для Температуры
void calcMesh::genEnergySLAE(valarray<double>& Vx, valarray<double>& Vy)
{
	m_slae.zeroize();

	for (list<smplMeshElement*>::const_iterator item = m_elements.begin(); item != m_elements.end(); ++item)
		(*item)->addLocalEnergyMatrix(m_slae, Vx, Vy);

	for (map<long int, double>::const_iterator item = m_bc_t_nodeList.begin(); item != m_bc_t_nodeList.end(); ++item)
		m_slae.eliminationOfDOF((*item).first, (*item).second);

	return;
}

//сформировать СЛАУ для Температуры (нестационарный)
void calcMesh::genEnergySLAE(valarray<double>& Vx, valarray<double>& Vy, double sigma, valarray<double>& lastStep)
{
	m_slae.zeroize();

	for (list<smplMeshElement*>::const_iterator item = m_elements.begin(); item != m_elements.end(); ++item)
	{
		(*item)->addLocalEnergyMatrix(m_slae, Vx, Vy);
		(*item)->addLocalTransientMatrix(m_slae, -sigma, lastStep);
	}

	for (map<long int, double>::const_iterator item = m_bc_t_nodeList.begin(); item != m_bc_t_nodeList.end(); ++item)
		m_slae.eliminationOfDOF((*item).first, (*item).second);

	return;
}


//сформировать СЛАУ для Вихря
void calcMesh::genVortexSLAE(valarray<double>& Vx, valarray<double>& Vy, valarray<double>& T, valarray<double>& aW)
{
	m_slae.zeroize();

	for (list<smplMeshElement*>::const_iterator item = m_elements.begin(); item != m_elements.end(); ++item)
		(*item)->addLocalVortexMatrix(m_slae, Vx, Vy, T);

	for (map<long int, double>::const_iterator item = m_bc_rs_nodeList.begin(); item != m_bc_rs_nodeList.end(); ++item)
		m_slae.eliminationOfDOF((*item).first, aW[(*item).first]);

	for (map<long int, double>::const_iterator item = m_bc_fs_nodeList.begin(); item != m_bc_fs_nodeList.end(); ++item)
		m_slae.eliminationOfDOF((*item).first, 0);

	for (map<long int, double>::const_iterator item = m_bc_fss_nodeList.begin(); item != m_bc_fss_nodeList.end(); ++item)
		m_slae.eliminationOfDOF((*item).first, aW[(*item).first]);
}

//сформировать СЛАУ для Вихря (нестационарный)
void calcMesh::genVortexSLAE(valarray<double>& Vx, valarray<double>& Vy, valarray<double>& T, valarray<double>& aW, double sigma, valarray<double>& lastStep)
{
	m_slae.zeroize();

	for (list<smplMeshElement*>::const_iterator item = m_elements.begin(); item != m_elements.end(); ++item)
	{
		(*item)->addLocalVortexMatrix(m_slae, Vx, Vy, T);
		(*item)->addLocalTransientMatrix(m_slae, sigma, lastStep);
	}

	for (map<long int, double>::const_iterator item = m_bc_rs_nodeList.begin(); item != m_bc_rs_nodeList.end(); ++item)
		m_slae.eliminationOfDOF((*item).first, aW[(*item).first]);
}

//сформировать СЛАУ для Функции тока
void calcMesh::genStreamSLAE(valarray<double>& Vortex)
{
	m_slae.zeroize();

	for (list<smplMeshElement*>::const_iterator item = m_elements.begin(); item != m_elements.end(); ++item)
		(*item)->addLocalStreamMatrix(m_slae, Vortex);

	for (map<long int, double>::const_iterator item = m_bc_rs_nodeList.begin(); item != m_bc_rs_nodeList.end(); ++item)
		m_slae.eliminationOfDOF((*item).first, 0.0);

	for (map<long int, double>::const_iterator item = m_bc_fs_nodeList.begin(); item != m_bc_fs_nodeList.end(); ++item)
		m_slae.eliminationOfDOF((*item).first, 0.0);
}

//сформировать СЛАУ для горизонтальной скорости
void calcMesh::genHorizontalVelocitySLAE(valarray<double>& Stream)
{
	m_slae.zeroize();

	for (list<smplMeshElement*>::const_iterator item = m_elements.begin(); item != m_elements.end(); ++item)
		(*item)->addLocalHorizontalVelocityMatrix(m_slae, Stream);

	for (map<long int, double>::const_iterator item = m_bc_rs_nodeList.begin(); item != m_bc_rs_nodeList.end(); ++item)
		m_slae.eliminationOfDOF((*item).first, 0.0);
}

//сформировать СЛАУ для вертикальной скорости
void calcMesh::genVerticalVelocitySLAE(valarray<double>& Stream)
{
	m_slae.zeroize();

	for (list<smplMeshElement*>::const_iterator item = m_elements.begin(); item != m_elements.end(); ++item)
		(*item)->addLocalVerticalVelocityMatrix(m_slae, Stream);

	for (map<long int, double>::const_iterator item = m_bc_rs_nodeList.begin(); item != m_bc_rs_nodeList.end(); ++item)
		m_slae.eliminationOfDOF((*item).first, 0.0);

}

//сформировать СЛАУ для КУ вихря
void calcMesh::genAltVortexSLAE(valarray<double>& Vx, valarray<double>& Vy)
{
	m_slae.zeroize();

	for (list<smplMeshElement*>::const_iterator item = m_elements.begin(); item != m_elements.end(); ++item)
		(*item)->addLocalAltVortexMatrix(m_slae, Vx, Vy);
}

#endif