using Gurobi;
using System.Text;

namespace Algorithms
{
    public class GurobiExample
    {
        public string Example()
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                //GRBEnv env = new GRBEnv("mip1.log");
                GRBEnv env = new GRBEnv();
                GRBModel model = new GRBModel(env);

                GRBVar x = model.AddVar(0.0, 1.0, 0.0, GRB.BINARY, "x");
                GRBVar y = model.AddVar(0.0, 1.0, 0.0, GRB.BINARY, "y");
                GRBVar z = model.AddVar(0.0, 1.0, 0.0, GRB.BINARY, "z");

                // Set objective: maximize x + y + 2 z

                model.SetObjective(x + y + 2 * z, GRB.MAXIMIZE);

                // Add constraint: x + 2 y + 3 z <= 4

                model.AddConstr(x + 2 * y + 3 * z <= 4.0, "c0");

                // Add constraint: x + y >= 1

                model.AddConstr(x + y >= 1.0, "c1");

                // Optimize model

                model.Optimize();

               
                sb.AppendLine(x.VarName + " " + x.X);
                sb.AppendLine(y.VarName + " " + y.X);
                sb.AppendLine(z.VarName + " " + z.X);

                sb.AppendLine("Obj: " + model.ObjVal);

                // Dispose of model and env

                model.Dispose();
                env.Dispose();

            }
            catch (GRBException e)
            {
               //pokemon
            }
            return sb.ToString();
        }
    }
}
