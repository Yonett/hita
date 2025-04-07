#ifndef SF_SLAE_MATRIX
#define SF_SLAE_MATRIX 1

#include <valarray>

const double g_slae_neighborhoodOfZero	=	1e-30;

/*=============================================================================================================================================
Класс разреженно-строчной матрицы для СЛАУ. Матрица в целях оптимальности не абстрактная. Так как использование одновременно нескольких матриц
для СЛАУ в рамках одной программы весьма сомнительно, при необходимости изменения типа матрицы можно использовать одноименный тип-обертку.
===============================================================================================================================================*/
class slaeMatrix
{
protected:
	size_t	m_dimension;						//размерность матрицы

	std::valarray<size_t>	m_ig;				//массив указателей на строки
	std::valarray<size_t>	m_jg;				//массив положения в столбцах
	std::valarray<double>	m_ggl;				//массив для нижнего треугольника матрицы
	std::valarray<double>	m_ggu;				//массив для верхнего треугольника матрицы
	std::valarray<double>	m_di;				//массив для диагонали матрицы

	std::valarray<double>	m_ggl_lu;			//массив для нижнего треугольника факторизованной матрицы
	std::valarray<double>	m_ggu_lu;			//массив для верхнего треугольника факторизованной матрицы
	std::valarray<double>	m_di_lu;			//массив для диагонали факторизованной матрицы

	/*=========================================================================================================================================
	Специальная структура для приватного узкоспециализированного списка. Список используется при учете краевых условий первого рода, требующих
	обнуления строки матрицы, и служит для запоминания позиций обнуляемых элементов в верхнем треугольнике, что исключает необходимость их
	повторного поиска. Для случаев, когда нужно итеративно решать нелинейное уравнение, использование такого списка может сокращать общее время
	вычислений на 20-50 %%.
	===========================================================================================================================================*/

	struct posList
	{
		posList*	m_next;			//указатель на следующий элемент списка
		size_t		m_pos;			//номер позиции в строке
		size_t		m_row;			//номер строки

		//конструктор
		posList(const size_t pos, const size_t row, posList* next)
		{	m_pos	=	pos;	m_row	=	row;	m_next = next;	}

		//деструктор (сначала разбирает свой хвост, а затем себя)
		~posList()
		{	if (m_next)	delete m_next;	}
	};

	std::valarray<posList*>	m_posTable;			//массив списков для учета первого КУ

	//=========================================================================================================================================//

	//вернуть (i,j)-й элемент матрицы,  если искомого элемента нет в портрете, то вернуть нулевой элемент матрицы
	double& getElement (const size_t row, const size_t column)
	{
		//нулевой элемент матрицы
		static double nullElement = 0;

		//если нужен диагональный элемент
		if (row == column)		return row < m_dimension ? m_di[row] : nullElement;

		//проверяем, что индексы не выходят за границы матрицы
		if (row < 0 || column < 0 || row >= m_dimension || column >= m_dimension)			return nullElement;

		//если нужен элемент из нижнего треугольника
		if (row > column)
		{	for (size_t k = m_ig[row]; k < m_ig[row + 1]; ++k)	if (m_jg[k] == column)		return m_ggl[k];	}
		//иначе, если нужен элемент из верхнего треугольника
		else
		{	for (size_t k = m_ig[column]; k < m_ig[column + 1]; ++k)	if (m_jg[k] == row)	return m_ggu[k];	}

		//не нашли искомый элемент
		return nullElement;
	}

	//=========================================================================================================================================//

	//исключить степень свободы вычетами Гаусса
	void eliminationOfDOF_Gauss(const size_t num, const double value, std::valarray<double>& vector)
	{
		//проверяем, что номер диагонального элемента лежит в допустимых пределах
		if (num > m_dimension)	return;

		//ставим единицу на диагональ и значение в вектор правой части
		vector[num]	=	value;
		m_di[num]	=	1.0;

		size_t	j, k;

		//обнуляем левую полустроку и верхний полустолбец
		for (j = m_ig[num]; j < m_ig[num + 1]; ++j)
		{
			vector[m_jg[j]]	-=	vector[num] * m_ggu[j];
			m_ggl[j]		 =	m_ggu[j]	=	 0; 
		}

		//зануляем правую полустроку и нижний полустолбец
		if (!m_posTable[num])
		{
			for (k = num + 1; k < m_dimension; ++k)
				for (j = m_ig[k]; j < m_ig[k + 1]; ++j)
					if (m_jg[j] > num)
						j = m_ig[k + 1];
					else
						if (m_jg[j] == num)
						{
							vector[k]		-=	vector[num] * m_ggl[j];
							m_ggl[j]		=	m_ggu[j]	=	0;
							m_posTable[num]	=	new posList(j, k, m_posTable[num]);
						}
		}
		else
		{
			for(posList* cursor = m_posTable[num]; cursor; cursor = cursor->m_next)
			{
				vector[cursor->m_row]	-=	vector[num] * m_ggl[cursor->m_pos];
				m_ggl[cursor->m_pos]	=	m_ggu[cursor->m_pos]	=	0;
			}
		}
	}

	//=========================================================================================================================================//

	//запрещаем конструктор копирования
	slaeMatrix(const slaeMatrix& source)	{}
	//запрещаем оператор присваивания
	const slaeMatrix operator = (const slaeMatrix& source)	{}

	//=========================================================================================================================================//

public:

	//=========================================================================================================================================//

	//обнулить элементы матрицы
	void zeroize()
	{	m_ggl = 0;	m_ggu = 0;	m_di = 0;	}

	//=========================================================================================================================================//

	//пустой конструктор матрицы
	slaeMatrix()	{m_dimension = 0;};

	//=========================================================================================================================================//

	//конструктор матрицы по портрету
	slaeMatrix(const std::valarray<size_t>& ig, const std::valarray<size_t>& jg)
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
	}

	//=========================================================================================================================================//

	//деструктор
	~slaeMatrix()
	{	for(size_t i = 0; i < m_dimension; ++i)	delete m_posTable[i];	}

	//=========================================================================================================================================//

	//создать по подобию
	virtual slaeMatrix* create(const std::valarray<size_t>& ig, const std::valarray<size_t>& jg)
	{	return new slaeMatrix(ig, jg);	}

	//=========================================================================================================================================//

	//умножение матрицы на вектор
	void multiply  (const std::valarray<double>& vector, std::valarray<double>& result)
	{
		size_t i, j;

		//обрабатываем диагональ
		for (i = 0; i < m_dimension; ++i)
			result[i] = vector[i] * m_di[i];

		//обрабатываем нижний и верхний треугольники
		for (i = 0; i < m_dimension; ++i)
			for (j = m_ig[i]; j < m_ig[i + 1]; ++j)
			{	
				result[m_jg[j]]	+= vector[i] * m_ggu[j];
				result[i]		+= vector[m_jg[j]] * m_ggl[j];
			}
	}

	//=========================================================================================================================================//

	//умножение транспонированной матрицы на вектор
	void multiplyT (const std::valarray<double>& vector, std::valarray<double>& result)
	{
		size_t i, j;

		//обрабатываем диагональ
		for (i = 0; i < m_dimension; ++i)
			result[i] = vector[i] * m_di[i];

		//обрабатываем нижний и верхний треугольники
		for (i = 0; i < m_dimension; ++i)
			for (j = m_ig[i]; j < m_ig[i + 1]; ++j)
			{	
				result[m_jg[j]]	+= vector[i] * m_ggl[j];
				result[i]		+= vector[m_jg[j]] * m_ggu[j];
			}
	}

	//=========================================================================================================================================//

	//обнулить строку
	void makeIdentityRow(const size_t num)
	{
		//проверяем, что номер диагонального элемента лежит в допустимых пределах
		if (num > m_dimension)	return;

		//ставим единицу на диагональ
		m_di[num]	=	1.0;

		size_t	j, k;

		//обнуляем левую полустроку и верхний полустолбец
		for (j = m_ig[num]; j < m_ig[num + 1]; ++j)
			m_ggl[j] =	0; 

		//зануляем правую полустроку и нижний полустолбец
		if (!m_posTable[num])
		{
			for (k = num + 1; k < m_dimension; ++k)
				for (j = m_ig[k]; j < m_ig[k + 1]; ++j)
					if (m_jg[j] > num)
						j = m_ig[k + 1];
					else
						if (m_jg[j] == num)
						{
							m_ggu[j] = 0;
							m_posTable[num]	=	new posList(j, k, m_posTable[num]);
						}
		}
		else
		{
			for(posList* cursor = m_posTable[num]; cursor; cursor = cursor->m_next)
				m_ggu[cursor->m_pos]	=	0;
		}
	}

	//=========================================================================================================================================//

	//скопировать данные из другой матрицы
	bool copyValuesFrom(const slaeMatrix& source)
	{
		if (m_dimension != source.m_dimension)		return true;

		m_di	=	source.m_di;
		m_ggl	=	source.m_ggl;
		m_ggu	=	source.m_ggu;

		return false;
	}

	//=========================================================================================================================================//

	//скопировать матрицу
	void copyFrom(const slaeMatrix& source)
	{
		m_dimension	=	source.m_dimension;

		m_posTable.resize(m_dimension);
		m_posTable	=	0;
		m_ig		=	source.m_ig;
		m_jg		=	source.m_jg;
		m_di		=	source.m_di;
		m_ggl		=	source.m_ggl;
		m_ggu		=	source.m_ggu;

		m_di_lu		=	source.m_di;
		m_ggl_lu	=	source.m_ggl;
		m_ggu_lu	=	source.m_ggu;

		return;
	}

	//=========================================================================================================================================//

	//установить значение (i,j)-го элемента
	void setValue(const size_t row, const size_t column, const double value)
	{
		getElement(row, column)	=	value;
	}

	//=========================================================================================================================================//

	//добавить значение к (i,j)-му элементу
	void addValue(const size_t row, const size_t column, const double value)
	{
		getElement(row, column)	+=	value;
	}

	//=========================================================================================================================================//

	//взять размерность матрицы
	size_t getDimension()
	{
		return m_dimension;
	}

	//=========================================================================================================================================//

	//факторизация
	bool factor()
	{
		size_t i, j, k, m;

		m_ggl_lu = m_ggl;
		m_ggu_lu = m_ggu;
		m_di_lu = m_di;

		for (i = 0; i < m_dimension; ++i)
		{
			for (j = m_ig[i]; j < m_ig[i + 1]; ++j)
			{
				for (k = m_ig[i]; k < j; ++k)
					for (m = m_ig[m_jg[j]]; m < m_ig[m_jg[j] + 1]; ++m)
						if (m_jg[k] == m_jg[m])
						{
							m_ggl_lu[j] -= m_ggl_lu[k] * m_ggu_lu[m];
							m_ggu_lu[j] -= m_ggu_lu[k] * m_ggl_lu[m];
							m = m_ig[m_jg[j] + 1];
						}
						else if (m_jg[k] < m_jg[m])
							m = m_ig[m_jg[j] + 1];

				if (fabs(m_di_lu[m_jg[j]]) < g_slae_neighborhoodOfZero) return true;
				m_ggl_lu[j] = m_ggl_lu[j]/m_di_lu[m_jg[j]];
			}

			for (j = m_ig[i]; j < m_ig[i + 1]; ++j)
				m_di_lu[i] -= m_ggl_lu[j] * m_ggu_lu[j];
		}

		return false;
	}

	//=========================================================================================================================================//

	//прямой ход Гаусса
	bool forwardTrace(const std::valarray<double>& vector, std::valarray<double>& result)
	{
		result = vector;

		size_t i, j;

		for (i = 0; i < m_dimension; ++i)
		for (j = m_ig[i]; j < m_ig[i + 1]; ++j)
			result[i] -= result[m_jg[j]] * m_ggl_lu[j];

		return false;
	}

	//=========================================================================================================================================//

	//прямой ход Гаусса для транспонированной матрицы
	bool forwardTraceT(const std::valarray<double>& vector, std::valarray<double>& result)
	{
		result = vector;

		size_t i, j;

		for (i = m_dimension - 1; i >= 0 && i < m_dimension; --i)
		for (j = m_ig[i]; j < m_ig[i + 1]; ++j)
			result[m_jg[j]] -= result[i] * m_ggl_lu[j];

		return false;
	}

	//=========================================================================================================================================//

	//обратный ход Гаусса
	bool reverseTrace(const std::valarray<double>& vector, std::valarray<double>& result)
	{
		result = vector;

		size_t i, j;

		for (i = m_dimension - 1; i >= 0 && i < m_dimension; --i)
		{
			if (fabs(m_di_lu[i]) < g_slae_neighborhoodOfZero)	return true;
			result[i] = result[i]/m_di_lu[i];

			for (j = m_ig[i]; j < m_ig[i + 1]; ++j)
				result[m_jg[j]] -= result[i] * m_ggu_lu[j];
		}

		return false;
	}

	//=========================================================================================================================================//

	//обратный ход Гаусса для транспонированной матрицы
	bool reverseTraceT(const std::valarray<double>& vector, std::valarray<double>& result)
	{
		result = vector;
		
		size_t i, j;

		for (i = 0; i < m_dimension; ++i)
		{
			for (j = m_ig[i]; j < m_ig[i + 1]; ++j)
				result[i] -= result[m_jg[j]] * m_ggu_lu[j];

			if (fabs(m_di_lu[i]) < g_slae_neighborhoodOfZero)	return true;
			result[i] = result[i]/m_di_lu[i];
		}

		return false;
	}

};

#endif