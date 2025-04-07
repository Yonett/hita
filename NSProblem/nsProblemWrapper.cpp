#include "nsProblem.h"

extern "C" __declspec(dllexport) nsProblem* createNsProblem(
	double Gr,
	double Pr,
	double width,
	double height,
	long int sptCnt_w,
	long int sptCnt_h,
	bool heatedSide,
	double slaeEps,
	long int slaeMaxIter,
	double sEps,
	long int sMaxIter)
{
	return new nsProblem(Gr, Pr, width, height, sptCnt_w, sptCnt_h, heatedSide, slaeEps, slaeMaxIter, sEps, sMaxIter);
}

extern "C" __declspec(dllexport) void deleteNsProblem(nsProblem *problem)
{
	if (problem != NULL)
		delete problem;
}

extern "C" __declspec(dllexport) bool solveNsProblem(nsProblem* problem, double w)
{
	if (problem != NULL)
		return problem->solve(w, cout);
	return false;
}

extern "C" __declspec(dllexport) void printResultsNsProblem(nsProblem* problem, const char* folder)
{
	if (problem != NULL)
		problem->printResults(folder);
}