using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using XF;
using XF.Model;

namespace XF.Model {
   public class ParameterMapper<TMapper, TEntity, TMessage> 
      where TMapper : IEntityMapper, IArgumentSource {
      private readonly TMapper _projector;
      private readonly PropertyInfo _messageProperty;
      private readonly string _entityName;
      private string _entityProperty;
      private object _defaultValue;
      private readonly List<string> _concatenations = new List<string>();
      private MaskedType _maskType = MaskedType.None;
      private Func<object, object> _valueConversion;
      private Func<object[], object> _compTool;
      private string Name { get; set; }

      public List<string> NamedArguments { get; set; }

      public ParameterMapper(TMapper projector, PropertyInfo messageProperty) {
         _projector = projector;
         _messageProperty = messageProperty;
         _entityName = typeof(TEntity).Name;
      }

      public ParameterMapper<TMapper, TEntity, TMessage> EntityProperty(Expression<Func<TEntity, object>> entityExpression) {
         var entityProperty = ExpressionsHelper.GetMemberInfo(entityExpression) as PropertyInfo;
         if (entityProperty == null) return this;
         _entityProperty = entityProperty.Name;
         return this;
      }

      public ParameterMapper<TMapper, TEntity, TMessage> DefaultValue(object defaultValue) {
         _defaultValue = defaultValue;
         return this;
      }

      public ParameterMapper<TMapper, TEntity, TMessage> Named() {
         Name = string.Format("{0}.{1}", typeof (TMessage).Name, _messageProperty.Name);
         return this;
      }

      public ParameterMapper<TMapper, TEntity, TMessage> FormatAs(MaskedType maskType) {
         _maskType = maskType;
         return this;
      }

      public ParameterMapper<TMapper, TEntity, TMessage> Concatenate(string concatString) {
         _concatenations.Add(concatString);
         return this;
      }

      public ParameterMapper<TMapper, TEntity, TMessage> Concatenate(Expression<Func<TEntity, object>> concatExpression) {
         var concatProperty = ExpressionsHelper.GetMemberInfo(concatExpression) as PropertyInfo;
         if (concatProperty == null) return this;

         _concatenations.Add(string.Format(" {0}.{1}", string.Format("{0}_{1}", _entityName.ToLower(), _projector.JoinRefIdx), concatProperty.Name));
         return this;
      }

      public ParameterMapper<TMapper, TEntity, TMessage> Convert(Func<object, object> tool) {
         _valueConversion = tool;
         return this;
      }

      public ComputationMapper<TMapper, TEntity, TMessage> Compute(Func<object[], object> tool) {
         _compTool = tool;
         var compTool = new ComputationMapper<TMapper, TEntity, TMessage>(this);
         return compTool;
      }

      public TMapper Add() {
         var part = new ProjectionPart {
            MessageProperty = _messageProperty,
            EntityProperty = !string.IsNullOrEmpty(_entityProperty)
                                ? _entityProperty
                                : _defaultValue == null
                                     ? _messageProperty.Name
                                     : string.Empty,
            DefaultValue = _defaultValue,
            EntityName = string.Format("{0}_{1}", _entityName, _projector.JoinRefIdx),
            ParameterIdx = _projector.ParameterIdx++,
            MaskType = _maskType,
            Concatenations = _concatenations,
            ValueConversion = _valueConversion,
            Name = Name,
            ComputationTool = _compTool,
            NamedArguments = NamedArguments,
            ArgumentSource = _projector
         };
         _projector.AddParameterPart(part, !string.IsNullOrEmpty(Name));
         return _projector;
      }
   }
}