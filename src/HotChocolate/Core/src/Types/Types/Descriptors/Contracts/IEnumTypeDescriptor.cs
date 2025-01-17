﻿using System;
using HotChocolate.Language;
using HotChocolate.Types.Descriptors.Definitions;

namespace HotChocolate.Types
{
    public interface IEnumTypeDescriptor
        : IDescriptor<EnumTypeDefinition>
        , IFluent
    {
        /// <summary>
        /// Associates the enum type with a syntax node
        /// of the parsed GraphQL SDL.
        /// </summary>
        /// <param name="enumTypeDefinition">
        /// The the type definition node.
        /// </param>
        IEnumTypeDescriptor SyntaxNode(
            EnumTypeDefinitionNode enumTypeDefinition);

        /// <summary>
        /// Defines the name the enum type shall have.
        /// </summary>
        /// <param name="value">
        /// The name value.
        /// </param>
        IEnumTypeDescriptor Name(NameString value);

        /// <summary>
        /// Defines the description that the enum type shall have.
        /// </summary>
        /// <param name="value">
        /// The description value.
        /// </param>
        IEnumTypeDescriptor Description(string value);

        [Obsolete("Use `Value`.")]
        IEnumValueDescriptor Item<T>(T value);

        /// <summary>
        /// Defines a value that should be included on the enum type.
        /// </summary>
        /// <param name="value">
        /// The value to include.
        /// </param>
        IEnumValueDescriptor Value<T>(T value);

        [Obsolete("Use `BindValues`.")]
        IEnumTypeDescriptor BindItems(BindingBehavior behavior);

        IEnumTypeDescriptor BindValues(BindingBehavior behavior);

        /// <summary>
        /// Defines that all enum values have to be specified explicitly.
        /// </summary>
        IEnumTypeDescriptor BindValuesExplicitly();

        /// <summary>
        /// Defines that all enum values shall be inferred
        /// from the associated .Net type,
        /// </summary>
        IEnumTypeDescriptor BindValuesImplicitly();

        IEnumTypeDescriptor Directive<T>(
            T directiveInstance)
            where T : class;

        IEnumTypeDescriptor Directive<T>()
            where T : class, new();

        IEnumTypeDescriptor Directive(
            NameString name,
            params ArgumentNode[] arguments);
    }
}
