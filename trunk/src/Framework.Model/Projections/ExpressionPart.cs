using System.Reflection;

namespace XF.Model {
   public class ExpressionPart<TMapper> where TMapper : IExpressionMapper {
      private readonly TMapper _mapper;
      private readonly string _propertyName;
      private readonly string _criteria;

      public ExpressionPart(TMapper mapper, string propertyName, string criteria) {
         _mapper = mapper;
         _propertyName = propertyName;
         _criteria = criteria;
      }

      public TMapper IsNull() {
         var criteria = string.Format("{0} is null ", _criteria);
         UpdateExpression(null, null, criteria);

         return _mapper;
      }

      public TMapper IsNotNull() {
         var criteria = string.Format("{0} is not null ", _criteria);
         UpdateExpression(null, null, criteria);

         return _mapper;
      }

      public TMapper Eq(object value) {
         if (string.IsNullOrEmpty(value.ToString())) return _mapper;

         var paramName = SetParameterName(_propertyName);
         var op = value.ToString().Contains("*") ? "like" : "=";
         var criteria = string.Format("{0} {1} :{2} ", _criteria, op, paramName);
         value = value.ToString().Contains("*") ? value.ToString().Replace('*', '%') : value;
         UpdateExpression(value, paramName, criteria);

         return _mapper;
      }

      public TMapper In(object value) {
         if (string.IsNullOrEmpty(value.ToString())) return _mapper;

         var paramName = SetParameterName(_propertyName);
         var criteria = string.Format("{0} in (:{1}) ", _criteria, paramName);
         UpdateExpression(value, paramName, criteria);

         return _mapper;
      }

      public TMapper GT(object value) {
         if (string.IsNullOrEmpty(value.ToString())) return _mapper;

         var paramName = SetParameterName(_propertyName);
         var criteria = string.Format("{0} > :{1} ", _criteria, paramName);
         UpdateExpression(value, paramName, criteria);

         return _mapper;
      }

      public TMapper GE(object value) {
         if (string.IsNullOrEmpty(value.ToString())) return _mapper;

         var paramName = SetParameterName(_propertyName);
         var criteria = string.Format("{0} >= :{1} ", _criteria, paramName);
         UpdateExpression(value, paramName, criteria);

         return _mapper;
      }

      public TMapper LT(object value) {
         if (string.IsNullOrEmpty(value.ToString())) return _mapper;

         var paramName = SetParameterName(_propertyName);
         var criteria = string.Format("{0} < :{1} ", _criteria, paramName);
         UpdateExpression(value, paramName, criteria);

         return _mapper;
      }

      public TMapper LE(object value) {
         if (string.IsNullOrEmpty(value.ToString())) return _mapper;

         var paramName = SetParameterName(_propertyName);
         var criteria = string.Format("{0} <= :{1} ", _criteria, paramName);
         UpdateExpression(value, paramName, criteria);

         return _mapper;
      }

      private void UpdateExpression(object value, string paramName, string criteria) {
         _mapper.CriteriaList.Add(criteria);
         if (paramName == null) return;
         _mapper.CriteriaParameters.Add(paramName, value);
      }

      private string SetParameterName(string propertyName) {
         return string.Format("{0}{1}",
                              _mapper.EntityName.ToLower(),
                              propertyName);
      }

   }
}