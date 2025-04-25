#ifndef ns_problem_mdl
#define ns_problem_mdl 1

#include "calcMesh.h"
#include <fstream>
#include <sstream>
#include <filesystem>
#include <time.h>

using namespace std;

/*==========================================================================================================================================
Класс-решатель задачи о термогравитационной конвекции в прямоугольной каверне
============================================================================================================================================*/
class nsProblem
{
public:
	//конструктор
	nsProblem(double Gr, double Pr, double width, double height, long int sptCnt_w, long int sptCnt_h, bool heatedSide, double slaeEps, long int slaeMaxIter, double sEps, long int sMaxIter )
	{
		m_mesh.genMesh(Gr, Pr, width, height, sptCnt_w,  sptCnt_h, heatedSide);
		m_mesh.genMatrix();
		m_mesh.getSLAE().setEpsilon(slaeEps);
		m_mesh.getSLAE().setMaxIter(slaeMaxIter);

		m_vortex.resize(m_mesh.getNodesCount(), 0);
		m_stream.resize(m_mesh.getNodesCount(), 0);
		m_heat.resize(m_mesh.getNodesCount(), 0);
		m_vx.resize(m_mesh.getNodesCount(), 0);
		m_vy.resize(m_mesh.getNodesCount(), 0);
		m_w.resize(m_mesh.getNodesCount(), 0);

		m_maxIter = sMaxIter;
		m_epsilon = sEps;
	}

	//установить начальные значения
	void setInitialApproximation(const char* dirPath)
	{
		double x, y, v;
		size_t iteration;
		char filePath[256];

		sprintf_s(filePath, 256, "%s\\Heat.txt", dirPath);
		ifstream inHeat(filePath);

		for (iteration = 0; iteration < m_heat.size() && inHeat; iteration++)
		{
			inHeat >> x >> y >> v;
			m_heat[iteration] = v;
		}
		inHeat.close();

		sprintf_s(filePath, 256, "%s\\Stream.txt", dirPath);
		ifstream inStream(filePath);

		for (iteration = 0; iteration < m_stream.size() && inStream; iteration++)
		{
			inStream >> x >> y >> v;
			m_stream[iteration] = v;
		}
		inStream.close();

		sprintf_s(filePath, 256, "%s\\Vortex.txt", dirPath);
		ifstream inVortex(filePath);

		for (iteration = 0; iteration < m_vortex.size() && inVortex; iteration++)
		{
			inVortex >> x >> y >> v;
			m_vortex[iteration] = v;
		}
		inVortex.close();

		sprintf_s(filePath, 256, "%s\\Vx.txt", dirPath);
		ifstream inVx(filePath);

		for (iteration = 0; iteration < m_vx.size() && inVx; iteration++)
		{
			inVx >> x >> y >> v;
			m_vx[iteration] = v;
		}
		inVx.close();

		sprintf_s(filePath, 256, "%s\\Vy.txt", dirPath);
		ifstream inVy(filePath);

		for (iteration = 0; iteration < m_vy.size() && inVy; iteration++)
		{
			inVy >> x >> y >> v;
			m_vy[iteration] = v;
		}
		inVy.close();

		return;
	}

	//добавить флуктуацию в поле температуры
	void addFluctuation(double percent)
	{
		srand((unsigned int)time(0));

		for (size_t iteration = 0; iteration < m_heat.size(); iteration++)
			m_heat[iteration] += rand()*(percent/100.)/RAND_MAX;

		return;
	}

	//решить систему уравнений
	bool solve(double w, ostream& out)
	{
		long int iteration = 0;			//счетчик итераций
		size_t i;						//счетчик циклов

		double norma_t = 1e+16;
		double norma_w = 0;
		double norma_s = 0;
		double norma   = 1;

		valarray<double>	heat_l;		//температура
		valarray<double>	vortex_l;	//вихрь
		valarray<double>	stream_l;	//функция тока

		m_mesh.getSLAE().setOutStream(out);

		vortex_l	=	m_vortex;
		stream_l	=	m_stream;
		heat_l		=	m_heat;

		while (iteration < m_maxIter)
		{
			out << "\n===========================================> " << iteration << " <======> " << sqrt((norma_t + norma_w + norma_s)/norma) << " <===\n\n";
			++iteration;

			out << "Creating the SLAE for Energy...\r";
			m_mesh.genEnergySLAE(m_vx, m_vy);
			out << "The SLAE for Energy was created.\n";
			
			out << "Solving the SLAE for Energy...\r";
			m_mesh.getSLAE().bcg_lu(m_heat);

			for (i = 0, norma_t	= 0, norma = 0; i < m_heat.size(); ++i)
			{
				heat_l[i] = m_heat[i];
				m_heat[i] = (1.0 - w) * m_heat[i] + w * m_mesh.getSLAE().getResult()[i];

				norma_t += (heat_l[i] - m_mesh.getSLAE().getResult()[i]) * (heat_l[i] - m_mesh.getSLAE().getResult()[i]);
				norma   += heat_l[i] * heat_l[i];
			}

			out << "Creating the SLAE for Vortex...\r";
			m_mesh.genVortexSLAE(m_vx, m_vy, m_heat, m_w);
			out << "The SLAE for Vortex was created.\n";
			
			out << "Solving the SLAE for Vortex...\r";
			m_mesh.getSLAE().bcg_lu(m_vortex);			

			for (i = 0, norma_w	= 0; i < m_vortex.size(); ++i)
			{
				vortex_l[i] = m_vortex[i];
				m_vortex[i] = (1.0 - w) * m_vortex[i] + w * m_mesh.getSLAE().getResult()[i];

				norma_w += (vortex_l[i] - m_mesh.getSLAE().getResult()[i]) * (vortex_l[i] - m_mesh.getSLAE().getResult()[i]);
				norma   += vortex_l[i] * vortex_l[i];
			}

			out << "Creating the SLAE for Stream...\r";
			m_mesh.genStreamSLAE(m_vortex);
			out << "The SLAE for Stream was created.\n";
			
			out << "Solving the SLAE for Stream...\r";
			m_mesh.getSLAE().bcg_lu(m_stream);

			for (i = 0, norma_s	= 0; i < m_stream.size(); ++i)
			{
				stream_l[i] = m_stream[i];
				m_stream[i] = (1.0 - w) * m_stream[i] + w * m_mesh.getSLAE().getResult()[i];

				norma_s += (stream_l[i] - m_mesh.getSLAE().getResult()[i]) * (stream_l[i] - m_mesh.getSLAE().getResult()[i]);
				norma   += stream_l[i] * stream_l[i];
			}

			out << "Creating the SLAE for horizontal velocity...\r";
			m_mesh.genHorizontalVelocitySLAE(m_stream);
			out << "The SLAE for horizontal velocity was created.\n";
			
			out << "Solving the SLAE for horizontal velocity...\r";
			m_mesh.getSLAE().bcg_lu(m_vx);
			m_vx = m_mesh.getSLAE().getResult();

			out << "Creating the SLAE for vertical velocity...\r";
			m_mesh.genVerticalVelocitySLAE(m_stream);
			out << "The SLAE for vertical velocity was created.\n";
			
			out << "Solving the SLAE for vertical velocity...\r";
			m_mesh.getSLAE().bcg_lu(m_vy);
			m_vy = m_mesh.getSLAE().getResult();

			if ((norma_t + norma_w + norma_s)/norma < m_epsilon*m_epsilon)	break;

			out << "Creating the SLAE for boundary vortex...\r";
			m_mesh.genAltVortexSLAE(m_vx, m_vy);
			out << "The SLAE for boundary vortex was created.\n";
			
			out << "Solving the SLAE for boundary vortex...\r";
			m_mesh.getSLAE().bcg_lu(m_w);
			m_w = m_mesh.getSLAE().getResult();
		}

		if (iteration == m_maxIter)			return true;
		out << "The system was solved" << "\t norm = " << sqrt((norma_t + norma_w + norma_s)/norma) << "\t iteration = " << iteration << "\n\n";

		return false;
	}

	//решить систему уравнений (+ method of false transient)
	bool solve(double w, ostream& out, double sigma, long int maxIter)
	{
		long int iteration = 0;			//счетчик итераций
		long int iteration_t = 0;		//счетчик итераций по времени
		size_t i;						//счетчик циклов

		double t_norma = 1e+16;
		double norma_t = 1e+16;
		double norma_w = 0;
		double norma_s = 0;
		double norma   = 1;
		

		valarray<double>	heat_l;		//температура
		valarray<double>	vortex_l;	//вихрь
		valarray<double>	stream_l;	//функция тока

		valarray<double>	l_heat;		//температура
		valarray<double>	l_vortex;	//вихрь

		m_mesh.getSLAE().setOutStream(out);

		l_vortex	=	m_vortex;
		l_heat		=	m_heat;

		while (iteration_t < maxIter)		
		{
			++iteration_t;

			vortex_l	=	m_vortex;
			stream_l	=	m_stream;
			heat_l		=	m_heat;
			iteration	=	0;

			while (iteration < m_maxIter)
			{
				out << "\n======> "<< iteration_t << " <======> " << t_norma << " <======> " << iteration << " <======> " << sqrt((norma_t + norma_w + norma_s)/norma) << " <===\n\n";
				++iteration;

				out << "Creating the SLAE for Energy...\r";
				m_mesh.genEnergySLAE(m_vx, m_vy, sigma, l_heat);
				out << "The SLAE for Energy was created.\n";
			
				out << "Solving the SLAE for Energy...\r";
				m_mesh.getSLAE().bcg_lu(m_heat);

				for (i = 0, norma_t	= 0, norma = 0; i < m_heat.size(); ++i)
				{
					heat_l[i] = m_heat[i];
					m_heat[i] = (1.0 - w) * m_heat[i] + w * m_mesh.getSLAE().getResult()[i];

					norma_t += (heat_l[i] - m_mesh.getSLAE().getResult()[i]) * (heat_l[i] - m_mesh.getSLAE().getResult()[i]);
					norma   += heat_l[i] * heat_l[i];
				}

				out << "Creating the SLAE for Vortex...\r";
				m_mesh.genVortexSLAE(m_vx, m_vy, m_heat, m_w, sigma, l_vortex);
				out << "The SLAE for Vortex was created.\n";
			
				out << "Solving the SLAE for Vortex...\r";
				m_mesh.getSLAE().bcg_lu(m_vortex);			

				for (i = 0, norma_w	= 0; i < m_vortex.size(); ++i)
				{
					vortex_l[i] = m_vortex[i];
					m_vortex[i] = (1.0 - w) * m_vortex[i] + w * m_mesh.getSLAE().getResult()[i];

					norma_w += (vortex_l[i] - m_mesh.getSLAE().getResult()[i]) * (vortex_l[i] - m_mesh.getSLAE().getResult()[i]);
					norma   += vortex_l[i] * vortex_l[i];
				}

				out << "Creating the SLAE for Stream...\r";
				m_mesh.genStreamSLAE(m_vortex);
				out << "The SLAE for Stream was created.\n";
			
				out << "Solving the SLAE for Stream...\r";
				m_mesh.getSLAE().bcg_lu(m_stream);

				for (i = 0, norma_s	= 0; i < m_stream.size(); ++i)
				{
					stream_l[i] = m_stream[i];
					m_stream[i] = (1.0 - w) * m_stream[i] + w * m_mesh.getSLAE().getResult()[i];

					norma_s += (stream_l[i] - m_mesh.getSLAE().getResult()[i]) * (stream_l[i] - m_mesh.getSLAE().getResult()[i]);
					norma   += stream_l[i] * stream_l[i];
				}

				out << "Creating the SLAE for horizontal velocity...\r";
				m_mesh.genHorizontalVelocitySLAE(m_stream);
				out << "The SLAE for horizontal velocity was created.\n";
			
				out << "Solving the SLAE for horizontal velocity...\r";
				m_mesh.getSLAE().bcg_lu(m_vx);
				m_vx = m_mesh.getSLAE().getResult();

				out << "Creating the SLAE for vertical velocity...\r";
				m_mesh.genVerticalVelocitySLAE(m_stream);
				out << "The SLAE for vertical velocity was created.\n";
			
				out << "Solving the SLAE for vertical velocity...\r";
				m_mesh.getSLAE().bcg_lu(m_vy);
				m_vy = m_mesh.getSLAE().getResult();

				if ((norma_t + norma_w + norma_s)/norma < m_epsilon*m_epsilon)	break;

				out << "Creating the SLAE for boundary vortex...\r";
				m_mesh.genAltVortexSLAE(m_vx, m_vy);
				out << "The SLAE for boundary vortex was created.\n";
			
				out << "Solving the SLAE for boundary vortex...\r";
				m_mesh.getSLAE().bcg_lu(m_w);
				m_w = m_mesh.getSLAE().getResult();
			}

			out << "The system was solved" << "\t norm = " << sqrt((norma_t + norma_w + norma_s)/norma) << "\t iteration = " << iteration << "\n\n";

			for (i = 0, t_norma	= 0, norma = 0; i < m_heat.size(); ++i)
			{
				t_norma += (l_heat[i] - m_heat[i]) * (l_heat[i] - m_heat[i]);
				norma   += m_heat[i] * m_heat[i];
				t_norma += (l_vortex[i] - m_vortex[i]) * (l_vortex[i] - m_vortex[i]);
				norma   += m_vortex[i] * m_vortex[i];

				l_heat[i]	= m_heat[i];
				l_vortex[i]	= m_vortex[i];
			}

			if (t_norma/norma < m_epsilon*m_epsilon)	break;
		}

		if (iteration_t == maxIter)			return true;
		out << "The system was solved" << "\t norm = " << sqrt(t_norma/norma) << "\t iteration = " << iteration_t << "\n\n";

		return false;
	}

	void printResults(string folder)
	{
		stringstream path;

		path << folder;
		filesystem::create_directory(path.str());

		path.str("");
		path.clear();
		path << folder << "\\" << "Heat.txt";

		//ofstream outHeat("Result\\Heat.txt");
		ofstream outHeat(path.str());
		outHeat.precision(16);
		outHeat.setf(ios_base::scientific);

		getMesh().toStream(outHeat, getHeat());
		outHeat.close();

		path.str("");
		path.clear();
		path << folder << "\\" << "Stream.txt";
		ofstream outStream(path.str());

		//ofstream outStream("Result\\Stream.txt");
		outStream.precision(16);
		outStream.setf(ios_base::scientific);

		getMesh().toStream(outStream, getStream());
		outStream.close();

		path.str("");
		path.clear();
		path << folder << "\\" << "Vortex.txt";
		ofstream outVortex(path.str());

		//ofstream outVortex("Result\\Vortex.txt");
		outVortex.precision(16);
		outVortex.setf(ios_base::scientific);

		getMesh().toStream(outVortex, getVortex());
		outVortex.close();

		path.str("");
		path.clear();
		path << folder << "\\" << "Vx.txt";
		ofstream outVx(path.str());

		//ofstream outVx("Result\\Vx.txt");
		outVx.precision(16);
		outVx.setf(ios_base::scientific);

		getMesh().toStream(outVx, getVx());
		outVx.close();

		path.str("");
		path.clear();
		path << folder << "\\" << "Vy.txt";
		ofstream outVy(path.str());

		//ofstream outVy("Result\\Vy.txt");
		outVy.precision(16);
		outVy.setf(ios_base::scientific);

		getMesh().toStream(outVy, getVy());
		outVy.close();

		path.str("");
		path.clear();
		path << folder << "\\" << "Nodes.txt";
		ofstream outNodes(path.str());

		//ofstream outVy("Result\\Vy.txt");
		outNodes.precision(16);
		outNodes.setf(ios_base::scientific);

		getMesh().toStream(outNodes);
		outNodes.close();

		return;
	}

	//взять температуру
	valarray<double>& getHeat()			{	return m_heat;			}
	//взять вихрь
	valarray<double>& getVortex()		{	return m_vortex;		}
	//взять функцию тока
	valarray<double>& getStream()		{	return m_stream;		}
	//взять горизонтальную компоненту скорости
	valarray<double>& getVx()			{	return m_vx;			}
	//взять вертикальную компоненту скорости
	valarray<double>& getVy()			{	return m_vy;			}
	//взять сетку
	calcMesh& getMesh()					{	return m_mesh;			}

private:
	calcMesh			m_mesh;		//сетка расчетной области

	valarray<double>	m_heat;		//температура
	valarray<double>	m_vortex;	//вихрь
	valarray<double>	m_stream;	//функция тока
	valarray<double>	m_vx;		//горизонтальная компонента скорости
	valarray<double>	m_vy;		//вертикальная компонента скорости
	valarray<double>	m_w;		//вихрь для КУ

	long int			m_maxIter;	//максимальное количество итераций решения системы уравнений
	double				m_epsilon;	//точность решения системы уравнений
};

#endif