#ifndef SF_SLAE_MATRIX
#define SF_SLAE_MATRIX 1

#include <valarray>

const double g_slae_neighborhoodOfZero	=	1e-30;

/*=============================================================================================================================================
����� ����������-�������� ������� ��� ����. ������� � ����� ������������� �� �����������. ��� ��� ������������� ������������ ���������� ������
��� ���� � ������ ����� ��������� ������ �����������, ��� ������������� ��������� ���� ������� ����� ������������ ����������� ���-�������.
===============================================================================================================================================*/
class slaeMatrix
{
protected:
	size_t	m_dimension;						//����������� �������

	std::valarray<size_t>	m_ig;				//������ ���������� �� ������
	std::valarray<size_t>	m_jg;				//������ ��������� � ��������
	std::valarray<double>	m_ggl;				//������ ��� ������� ������������ �������
	std::valarray<double>	m_ggu;				//������ ��� �������� ������������ �������
	std::valarray<double>	m_di;				//������ ��� ��������� �������

	std::valarray<double>	m_ggl_lu;			//������ ��� ������� ������������ ��������������� �������
	std::valarray<double>	m_ggu_lu;			//������ ��� �������� ������������ ��������������� �������
	std::valarray<double>	m_di_lu;			//������ ��� ��������� ��������������� �������

	/*=========================================================================================================================================
	����������� ��������� ��� ���������� ����������������������� ������. ������ ������������ ��� ����� ������� ������� ������� ����, ���������
	��������� ������ �������, � ������ ��� ����������� ������� ���������� ��������� � ������� ������������, ��� ��������� ������������� ��
	���������� ������. ��� �������, ����� ����� ���������� ������ ���������� ���������, ������������� ������ ������ ����� ��������� ����� �����
	���������� �� 20-50 %%.
	===========================================================================================================================================*/

	struct posList
	{
		posList*	m_next;			//��������� �� ��������� ������� ������
		size_t		m_pos;			//����� ������� � ������
		size_t		m_row;			//����� ������

		//�����������
		posList(const size_t pos, const size_t row, posList* next)
		{	m_pos	=	pos;	m_row	=	row;	m_next = next;	}

		//���������� (������� ��������� ���� �����, � ����� ����)
		~posList()
		{	if (m_next)	delete m_next;	}
	};

	std::valarray<posList*>	m_posTable;			//������ ������� ��� ����� ������� ��

	//=========================================================================================================================================//

	//������� (i,j)-� ������� �������,  ���� �������� �������� ��� � ��������, �� ������� ������� ������� �������
	double& getElement (const size_t row, const size_t column)
	{
		//������� ������� �������
		static double nullElement = 0;

		//���� ����� ������������ �������
		if (row == column)		return row < m_dimension ? m_di[row] : nullElement;

		//���������, ��� ������� �� ������� �� ������� �������
		if (row < 0 || column < 0 || row >= m_dimension || column >= m_dimension)			return nullElement;

		//���� ����� ������� �� ������� ������������
		if (row > column)
		{	for (size_t k = m_ig[row]; k < m_ig[row + 1]; ++k)	if (m_jg[k] == column)		return m_ggl[k];	}
		//�����, ���� ����� ������� �� �������� ������������
		else
		{	for (size_t k = m_ig[column]; k < m_ig[column + 1]; ++k)	if (m_jg[k] == row)	return m_ggu[k];	}

		//�� ����� ������� �������
		return nullElement;
	}

	//=========================================================================================================================================//

	//��������� ������� ������� �������� ������
	void eliminationOfDOF_Gauss(const size_t num, const double value, std::valarray<double>& vector)
	{
		//���������, ��� ����� ������������� �������� ����� � ���������� ��������
		if (num > m_dimension)	return;

		//������ ������� �� ��������� � �������� � ������ ������ �����
		vector[num]	=	value;
		m_di[num]	=	1.0;

		size_t	j, k;

		//�������� ����� ���������� � ������� �����������
		for (j = m_ig[num]; j < m_ig[num + 1]; ++j)
		{
			vector[m_jg[j]]	-=	vector[num] * m_ggu[j];
			m_ggl[j]		 =	m_ggu[j]	=	 0; 
		}

		//�������� ������ ���������� � ������ �����������
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

	//��������� ����������� �����������
	slaeMatrix(const slaeMatrix& source)	{}
	//��������� �������� ������������
	const slaeMatrix operator = (const slaeMatrix& source)	{}

	//=========================================================================================================================================//

public:

	//=========================================================================================================================================//

	//�������� �������� �������
	void zeroize()
	{	m_ggl = 0;	m_ggu = 0;	m_di = 0;	}

	//=========================================================================================================================================//

	//������ ����������� �������
	slaeMatrix()	{m_dimension = 0;};

	//=========================================================================================================================================//

	//����������� ������� �� ��������
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

	//����������
	~slaeMatrix()
	{	for(size_t i = 0; i < m_dimension; ++i)	delete m_posTable[i];	}

	//=========================================================================================================================================//

	//������� �� �������
	virtual slaeMatrix* create(const std::valarray<size_t>& ig, const std::valarray<size_t>& jg)
	{	return new slaeMatrix(ig, jg);	}

	//=========================================================================================================================================//

	//��������� ������� �� ������
	void multiply  (const std::valarray<double>& vector, std::valarray<double>& result)
	{
		size_t i, j;

		//������������ ���������
		for (i = 0; i < m_dimension; ++i)
			result[i] = vector[i] * m_di[i];

		//������������ ������ � ������� ������������
		for (i = 0; i < m_dimension; ++i)
			for (j = m_ig[i]; j < m_ig[i + 1]; ++j)
			{	
				result[m_jg[j]]	+= vector[i] * m_ggu[j];
				result[i]		+= vector[m_jg[j]] * m_ggl[j];
			}
	}

	//=========================================================================================================================================//

	//��������� ����������������� ������� �� ������
	void multiplyT (const std::valarray<double>& vector, std::valarray<double>& result)
	{
		size_t i, j;

		//������������ ���������
		for (i = 0; i < m_dimension; ++i)
			result[i] = vector[i] * m_di[i];

		//������������ ������ � ������� ������������
		for (i = 0; i < m_dimension; ++i)
			for (j = m_ig[i]; j < m_ig[i + 1]; ++j)
			{	
				result[m_jg[j]]	+= vector[i] * m_ggl[j];
				result[i]		+= vector[m_jg[j]] * m_ggu[j];
			}
	}

	//=========================================================================================================================================//

	//�������� ������
	void makeIdentityRow(const size_t num)
	{
		//���������, ��� ����� ������������� �������� ����� � ���������� ��������
		if (num > m_dimension)	return;

		//������ ������� �� ���������
		m_di[num]	=	1.0;

		size_t	j, k;

		//�������� ����� ���������� � ������� �����������
		for (j = m_ig[num]; j < m_ig[num + 1]; ++j)
			m_ggl[j] =	0; 

		//�������� ������ ���������� � ������ �����������
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

	//����������� ������ �� ������ �������
	bool copyValuesFrom(const slaeMatrix& source)
	{
		if (m_dimension != source.m_dimension)		return true;

		m_di	=	source.m_di;
		m_ggl	=	source.m_ggl;
		m_ggu	=	source.m_ggu;

		return false;
	}

	//=========================================================================================================================================//

	//����������� �������
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

	//���������� �������� (i,j)-�� ��������
	void setValue(const size_t row, const size_t column, const double value)
	{
		getElement(row, column)	=	value;
	}

	//=========================================================================================================================================//

	//�������� �������� � (i,j)-�� ��������
	void addValue(const size_t row, const size_t column, const double value)
	{
		getElement(row, column)	+=	value;
	}

	//=========================================================================================================================================//

	//����� ����������� �������
	size_t getDimension()
	{
		return m_dimension;
	}

	//=========================================================================================================================================//

	//������������
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

	//������ ��� ������
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

	//������ ��� ������ ��� ����������������� �������
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

	//�������� ��� ������
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

	//�������� ��� ������ ��� ����������������� �������
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