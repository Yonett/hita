#ifndef SF_SLAE
#define SF_SLAE 1

#include <list>
#include <iostream>
#include "SmplMatrix.h"

using namespace std;

/*=============================================================================================================================================
Класс СЛАУ. Представляет собой класс матрицы расширенной вектором правой части и операциями решения СЛАУ.
===============================================================================================================================================*/

class slae : public slaeMatrix
{
protected:
	valarray<double>		m_vector;			//массив для вектора правой части

	valarray<double>		m_result;			//вектор ответа
	valarray<double>		m_r1;				//вектор алгоритма BCG
	valarray<double>		m_r2;				//вектор алгоритма BCG
	valarray<double>		m_p1;				//вектор алгоритма BCG
	valarray<double>		m_p2;				//вектор алгоритма BCG
	valarray<double>		m_q;				//вспомогательный вектор BCG
	valarray<double>		m_Ax;				//вспомогательный вектор BCG
	valarray<double>		m_ATx;				//вспомогательный вектор BCG

	size_t					m_maxIter;			//максимальное количество итераций
	double					m_epsilon;			//точность получаемого решения
	double					m_norma;			//норма полученного решения

	ostream*				m_out;				//поток вывода информационных сообщений

	//=========================================================================================================================================//

	//запрещаем конструктор копирования
	slae(const slaeMatrix& source) : slaeMatrix(source)	{}
	slae(const slae& source) : slaeMatrix(source)	{}
	//запрещаем оператор присваивания
	const slae operator = (const slae& source)	{}

	//=========================================================================================================================================//

public:

	//=========================================================================================================================================//

	//пустой конструктор
	slae() : slaeMatrix() {		m_maxIter = 0;	m_epsilon = 0;	m_norma	= 0;	m_out = 0;	};

	//=========================================================================================================================================//

	//конструктор
	slae(const std::valarray<size_t>& ig, const std::valarray<size_t>& jg) : slaeMatrix(ig, jg)
	{	
		m_vector.resize(ig.size() - 1, 0);

		m_result.resize(ig.size() - 1, 0);
		m_r1.resize(ig.size() - 1, 0);
		m_r2.resize(ig.size() - 1, 0);
		m_p1.resize(ig.size() - 1, 0);
		m_p2.resize(ig.size() - 1, 0);
		m_q.resize(ig.size() - 1, 0);
		m_Ax.resize(ig.size() - 1, 0);
		m_ATx.resize(ig.size() - 1, 0);

		m_maxIter = 0;	m_epsilon = 0;	m_norma	= 0;	m_out = 0;
	}

	//=========================================================================================================================================//

	//обнулить элементы СЛАУ
	void zeroize()
	{	m_di = 0;	m_ggl = 0;	m_ggu = 0;	m_vector = 0;	}

	//=========================================================================================================================================//

	//взять ссылку на элемент матрицы СЛАУ
	double& operator() (const size_t row, const size_t column)		{	return getElement(row, column);						}

	//=========================================================================================================================================//

	//взять ссылку на элемент вектора правой части СЛАУ
	double& operator[] (const size_t row)							{	return m_vector[row % m_dimension];					}

	//=========================================================================================================================================//

	//исключить степень свободы вычетами Гаусса
	void eliminationOfDOF(const size_t num, const double value)		{	eliminationOfDOF_Gauss(num, value, m_vector);		}

	//=========================================================================================================================================//

	//проинициализировать матрицу
	void init(const std::valarray<size_t>& ig, const std::valarray<size_t>& jg)
	{
		m_dimension	=	ig.size() - 1;
		if (m_dimension < 2)	return;

		m_ig = ig;				m_jg = jg;

		m_ggl.resize(m_ig[m_dimension], 0);
		m_ggu.resize(m_ig[m_dimension], 0);
		m_di.resize(m_dimension, 0);

		m_ggl_lu.resize(m_ig[m_dimension], 0);
		m_ggu_lu.resize(m_ig[m_dimension], 0);
		m_di_lu.resize(m_dimension, 0);

		m_posTable.resize(m_dimension, 0);
		m_vector.resize(m_dimension, 0);

		m_result.resize(m_dimension, 0);
		m_r1.resize(m_dimension, 0);
		m_r2.resize(m_dimension, 0);
		m_p1.resize(m_dimension, 0);
		m_p2.resize(m_dimension, 0);
		m_q.resize(m_dimension, 0);
		m_Ax.resize(m_dimension, 0);
		m_ATx.resize(m_dimension, 0);
	}

	//=========================================================================================================================================//

	//вернуть вектор полученного решения
	valarray<double>&	getResult()				{	return m_result;				}
	//вернуть норму полученного решения
	double				getNorma()				{	return m_norma;					}
	//вернуть требуемую точность решения
	double				getEpsilon()			{	return m_epsilon;				}
	//вернуть максимальное количество итераций
	size_t				getMaxIter()			{	return	m_maxIter;				}

	//задать требуемую точность решения
	void	setEpsilon(const double epsilon)	{	m_epsilon = epsilon;			}
	//задать максимальное количество итераций
	void	setMaxIter(const size_t maxIter)	{	m_maxIter = maxIter;			}
	//задать поток для вывода информационных сообщений
	void	setOutStream(ostream& out)			{	m_out = &out;					}

	//=========================================================================================================================================//

	//решить СЛАУ методом BCGLU
	bool bcg_lu(const std::valarray<double>& approx, bool withFactorization = true)
	{
		if (m_dimension != approx.size() || approx.size() == 0 || !m_maxIter)		return true;
		if (withFactorization)	if (factor())										return true;

		size_t	iteration = 0, i = 0;
		double	scalar1	= 0;
		double	scalar2	= 0;
		double	norma	= 0;
		double	alpha	= 0;
		double	beta	= 0;

		//нулевой шаг алгоритма
		m_result	=	approx;
		multiply(approx, m_Ax);

		for (i = 0; i < m_dimension; ++i)
			m_r2[i] = m_vector[i] - m_Ax[i];

		if (forwardTrace(m_r2, m_r1))		return true;
		m_r2 = m_p1 = m_p2 = m_r1;

		for (i = 0; i < m_dimension; ++i)
			scalar1	+=  m_r1[i] * m_r2[i];

		//итерационный процесс
		while(iteration < m_maxIter)
		{
			++iteration;

			//вычисляем скалярные произведения
			if (reverseTrace(m_p1, m_Ax))	return true;
			multiply(m_Ax, m_q);
			if (forwardTrace(m_q, m_Ax))	return true;

			for (scalar2 = 0, i = 0; i < m_dimension; ++i)
				scalar2 += m_Ax[i] * m_p2[i];

			//проверяем целесообразность продолжения итерационного процесса
			if (fabs(scalar2) < g_slae_neighborhoodOfZero)	break;

			//вычисляем коэффициент
			alpha = scalar1/scalar2;

			//проверяем целесообразность продолжения итерационного процесса
			if (fabs(alpha) < g_slae_neighborhoodOfZero)	break;

			//вычисляем новые значения векторов
			if (forwardTraceT(m_p2, m_ATx))	return true;
			multiplyT(m_ATx, m_q);
			if (reverseTraceT(m_q, m_ATx))	return true;
			if (reverseTrace(m_p1, m_q))	return true;

			scalar2 = scalar1;

			for (scalar1 = 0, i = 0; i < m_dimension; ++i)
			{
				m_result[i]	=  m_result[i] + alpha * m_q[i];
				m_r1[i]		=  m_r1[i] - alpha * m_Ax[i];
				m_r2[i]		=  m_r2[i] - alpha * m_ATx[i];
				scalar1		+= m_r1[i] * m_r2[i];
			}

			//вычисляем норму вектора невязки
			multiply(m_result, m_q);

			for (m_norma = 0, norma = 0, i = 0; i < m_dimension; ++i)
			{
				m_norma +=	(m_vector[i] - m_q[i]) * (m_vector[i] - m_q[i]);
				norma	+=	m_vector[i] * m_vector[i];
			}

			//проверяем условие выхода
			if (m_norma/norma < m_epsilon*m_epsilon)		break;

			//проверяем целесообразность продолжения итерационного процесса
			if (fabs(scalar1) < g_slae_neighborhoodOfZero)	break;

			//вычисляем коэффициент
			beta = scalar1/scalar2;

			//проверяем целесообразность продолжения итерационного процесса
			if (fabs(beta) < g_slae_neighborhoodOfZero)		break;

			//вычисляем новые значения векторов
			for (i = 0; i < m_dimension; ++i)
			{
				m_p1[i]	= m_r1[i] + beta * m_p1[i];
				m_p2[i]	= m_r2[i] + beta * m_p2[i];
			}
		}

		if (iteration == m_maxIter)			return true;
		if (m_out != 0)	(*m_out) << "The SLAE was solved by BCG_LU" << "\t norm = " << m_norma << "\t iteration = " << iteration << "\n\n";

		return false;	
	}

	//=========================================================================================================================================//

	//решить СЛАУ методом LOSLU
	bool los_lu(const valarray<double>& approx, bool withFactorization = true)
	{
		if (m_dimension != approx.size() || approx.size() == 0 || !m_maxIter)		return true;
		if (withFactorization)	if (factor())										return true;

		size_t	iteration = 0, i = 0;
		double	scalar1	= 0;
		double	scalar2	= 0;
		double	norma	= 0;
		double	alpha	= 0;
		double	beta	= 0;

		//нулевой шаг алгоритма
		m_result	=	approx;
		multiply(approx, m_Ax);

		for (i = 0; i < m_dimension; ++i)
			m_r2[i] = m_vector[i] - m_Ax[i];

		if (forwardTrace(m_r2, m_r1))		return true;
		if (reverseTrace(m_r1, m_r2))		return true;

		multiply(m_r2, m_Ax);
		if (forwardTrace(m_Ax, m_p1))		return true;

		//итерационный процесс
		while(iteration < m_maxIter)
		{
			++iteration;

			//вычисляем скалярные произведения
			for (i = 0, scalar2 = 0, scalar1 = 0; i < m_dimension; ++i)
			{
				scalar2	+=  m_p1[i] * m_p1[i];
				scalar1	+=  m_p1[i] * m_r1[i];
			}

			//проверяем целесообразность продолжения итерационного процесса
			if (fabs(scalar2) < g_slae_neighborhoodOfZero)	break;

			//вычисляем коэффициент
			alpha = scalar1/scalar2;

			//проверяем целесообразность продолжения итерационного процесса
			if (fabs(alpha) < g_slae_neighborhoodOfZero)	break;

			//вычисляем новые значения векторов
			for (i = 0; i < m_dimension; ++i)
			{
				m_result[i]	=  m_result[i] + alpha * m_r2[i];
				m_r1[i]		=  m_r1[i]     - alpha * m_p1[i];
			}

			//вычисляем норму вектора невязки
			multiply(m_result, m_q);

			for (m_norma = 0, norma = 0, i = 0; i < m_dimension; ++i)
			{
				m_norma +=	(m_vector[i] - m_q[i])*(m_vector[i] - m_q[i]);
				norma	+=	m_vector[i] * m_vector[i];
			}

			//проверяем условие выхода
			if (m_norma/norma < m_epsilon*m_epsilon)		break;

			//вычисляем скалярное произведение
			if (reverseTrace(m_r1, m_p2))		return true;
			multiply(m_p2, m_Ax);
			if (forwardTrace(m_Ax, m_q))		return true;

			for (i = 0, scalar1 = 0; i < m_dimension; ++i)
				scalar1	+=  m_p1[i] * m_q[i];

			//вычисляем коэффициент
			beta = -scalar1/scalar2;

			//проверяем целесообразность продолжения итерационного процесса
			if (fabs(beta) < g_slae_neighborhoodOfZero)		break;

			//вычисляем новые значения векторов
			for (i = 0; i < m_dimension; ++i)
			{
				m_r2[i]	=	m_p2[i] + beta * m_r2[i];
				m_p1[i]	=	m_q[i]  + beta * m_p1[i];
			}
		}

		if (iteration == m_maxIter)				return true;
		if (m_out != 0)	(*m_out) << "The SLAE was solved by LOS_LU" << "\t norm = " << m_norma << "\t iteration = " << iteration << "\n\n";
		
		return false;	
	}

	//=========================================================================================================================================//

	//решить СЛАУ методом BCG
	bool bcg(const valarray<double>& approx, size_t maxIter_mul = 5)
	{
		if (m_dimension != approx.size() || approx.size() == 0 || !m_maxIter)		return true;

		size_t	iteration = 0, i = 0;
		double	scalar1	= 0;
		double	scalar2	= 0;
		double	norma	= 0;
		double	alpha	= 0;
		double	beta	= 0;

		//нулевой шаг алгоритма
		m_result	=	approx;
		multiply(approx, m_Ax);

		for (i = 0; i < m_dimension; ++i)
			m_r1[i] = m_vector[i] - m_Ax[i];

		m_p1 = m_p2 = m_r2 = m_r1;

		for (i = 0; i < m_dimension; ++i)
			scalar1	+=  m_r1[i] * m_r2[i];

		//итерационный процесс
		while(iteration < m_maxIter*maxIter_mul)
		{
			++iteration;

			//вычисляем скалярные произведения
			multiply(m_p1, m_Ax);

			for (scalar2 = 0, i = 0; i < m_dimension; ++i)
				scalar2 += m_Ax[i] * m_p2[i];

			//проверяем целесообразность продолжения итерационного процесса
			if (fabs(scalar2) < g_slae_neighborhoodOfZero)		break;

			//вычисляем коэффициент
			alpha = scalar1/scalar2;

			//проверяем целесообразность продолжения итерационного процесса
			if (fabs(alpha) < g_slae_neighborhoodOfZero)		break;

			//вычисляем новые значения векторов
			multiplyT(m_p2, m_ATx);

			scalar2 = scalar1;

			for (scalar1 = 0, i = 0; i < m_dimension; ++i)
			{
				m_result[i]	=  m_result[i] + alpha * m_p1[i];
				m_r1[i]		=  m_r1[i] - alpha * m_Ax[i];
				m_r2[i]		=  m_r2[i] - alpha * m_ATx[i];
				scalar1		+= m_r1[i] * m_r2[i];
			}

			//вычисляем норму вектора невязки
			multiply(m_result, m_q);

			for (m_norma = 0, norma = 0, i = 0; i < m_dimension; ++i)
			{
				m_norma +=	(m_vector[i] - m_q[i])*(m_vector[i] - m_q[i]);
				norma	+=	m_vector[i]*m_vector[i];
			}

			//проверяем условие выхода
			if (m_norma/norma < m_epsilon*m_epsilon)			break;

			//проверяем целесообразность продолжения итерационного процесса
			if (fabs(scalar1) < g_slae_neighborhoodOfZero)		break;

			//вычисляем коэффициент
			beta = scalar1/scalar2;

			//проверяем целесообразность продолжения итерационного процесса
			if (fabs(beta) < g_slae_neighborhoodOfZero)			break;

			//вычисляем новые значения векторов
			for (i = 0; i < m_dimension; ++i)
			{
				m_p1[i]	= m_r1[i] + beta * m_p1[i];
				m_p2[i]	= m_r2[i] + beta * m_p2[i];
			}
		}

		if (iteration == m_maxIter*maxIter_mul)		return true;
		if (m_out != 0)	(*m_out) << "The SLAE was solved by BCG" << "\t norm = " << m_norma << "\t iteration = " << iteration << "\n\n";
		
		return false;	
	}

	//=========================================================================================================================================//

	//решить СЛАУ методом LOS
	bool los(const valarray<double>& approx, size_t maxIter_mul = 5)
	{
		if (m_dimension != approx.size() || approx.size() == 0 || !m_maxIter)		return true;

		size_t	iteration = 0, i = 0;
		double	scalar1	= 0;
		double	scalar2	= 0;
		double	norma	= 0;
		double	alpha	= 0;
		double	beta	= 0;

		//нулевой шаг алгоритма
		m_result	=	approx;
		multiply(approx, m_Ax);

		for (i = 0; i < m_dimension; ++i)
			m_r1[i] = m_r2[i] = m_vector[i] - m_Ax[i];

		multiply(m_r1, m_p1);

		//итерационный процесс
		while(iteration < m_maxIter*maxIter_mul)
		{
			++iteration;

			//вычисляем скалярные произведения
			for (i = 0, scalar2 = 0, scalar1 = 0; i < m_dimension; ++i)
			{
				scalar2	+=  m_p1[i] * m_p1[i];
				scalar1	+=  m_p1[i] * m_r1[i];
			}

			//проверяем целесообразность продолжения итерационного процесса
			if (fabs(scalar2) < g_slae_neighborhoodOfZero)		break;

			//вычисляем коэффициент
			alpha = scalar1/scalar2;

			//проверяем целесообразность продолжения итерационного процесса
			if (fabs(alpha) < g_slae_neighborhoodOfZero)		break;

			//вычисляем новые значения векторов
			for (i = 0; i < m_dimension; ++i)
			{
				m_result[i]	=  m_result[i] + alpha * m_r2[i];
				m_r1[i]		=  m_r1[i]     - alpha * m_p1[i];
			}

			//вычисляем норму вектора невязки
			multiply(m_result, m_q);

			for (m_norma = 0, norma = 0, i = 0; i < m_dimension; ++i)
			{
				m_norma +=	(m_vector[i] - m_q[i])*(m_vector[i] - m_q[i]);
				norma	+=	m_vector[i] * m_vector[i];
			}

			//проверяем условие выхода
			if (m_norma/norma < m_epsilon*m_epsilon)			break;

			//вычисляем скалярное произведение
			multiply(m_r1, m_Ax);

			for (i = 0, scalar1 = 0; i < m_dimension; ++i)
				scalar1	+=  m_p1[i] * m_Ax[i];

			//вычисляем коэффициент
			beta = -scalar1/scalar2;

			//проверяем целесообразность продолжения итерационного процесса
			if (fabs(beta) < g_slae_neighborhoodOfZero)			break;

			//вычисляем новые значения векторов
			for (i = 0; i < m_dimension; ++i)
			{
				m_r2[i]	=	m_r1[i] + beta * m_r2[i];
				m_p1[i]	=	m_Ax[i]  + beta * m_p1[i];
			}
		}

		if (iteration == m_maxIter*maxIter_mul)	return true;
		if (m_out != 0)		(*m_out) << "The SLAE was solved by LOS" << "\t norm = " << m_norma << "\t iteration = " << iteration << "\n\n";
		
		return false;	
	}

	//=========================================================================================================================================//

	//решить СЛАУ
	bool solve(const valarray<double>& approx)
	{
		return bcg_lu(approx) ? los_lu(approx, false) ? bcg(approx) ? los(approx) : false : false : false;
	}
};


#endif