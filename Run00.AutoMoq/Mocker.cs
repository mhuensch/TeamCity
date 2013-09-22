using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Run00.AutoMoq
{
	public class Mocker<T> where T : class
	{
		public Mocker()
		{
			_registeredMocks = new Dictionary<Type, Mock>();
			_mockBehaviors = new Dictionary<Type, MockBehavior>();
			_defaultBehavior = MockBehavior.Strict;
		}

		public Mocker(MockBehavior defaultBehavior)
		{
			_registeredMocks = new Dictionary<Type, Mock>();
			_mockBehaviors = new Dictionary<Type, MockBehavior>();
			_defaultBehavior = defaultBehavior;
		}

		public void SetBehavior<TMock>(MockBehavior behavior) where TMock : class
		{
			if (_registeredMocks.Keys.Contains(typeof(TMock)))
				throw new InvalidOperationException("You may not set a mock behavior for a type that has already been defined");

			if (_mockBehaviors.Keys.Contains(typeof(TMock)))
				_mockBehaviors[typeof(TMock)] = behavior;
			else
				_mockBehaviors.Add(typeof(TMock), behavior);
		}

		public Mock<TMock> GetMock<TMock>() where TMock : class
		{
			var mockType = typeof(TMock);

			if (_registeredMocks.Keys.Contains(mockType) == false)
			{
				var mockBehavior = GetMockBehaviorForType(typeof(TMock));
				_registeredMocks.Add(mockType, new Mock<TMock>(mockBehavior));
			}

			return (Mock<TMock>)_registeredMocks[mockType];
		}

		public void ClearMock<TMock>() where TMock : class
		{
			var mockType = typeof(TMock);

			if (_registeredMocks.Keys.Contains(mockType) == false)
				return;

			_registeredMocks.Remove(mockType);
		}

		public T Create()
		{
			var objType = typeof(T);
			var container = new WindsorContainer();
			container.Register(Component.For(objType).ImplementedBy(objType));

			var paramTypes = objType
				.GetConstructors()
				.SelectMany(c => c.GetParameters())
				.Select(p => p.ParameterType)
				.Distinct();

			foreach (var paramType in paramTypes)
				container.Register(Component.For(paramType).UsingFactoryMethod(() => GetMockForType(paramType).Object));

			return container.Resolve<T>();
		}

		public void Verify()
		{
			foreach (var mock in _registeredMocks)
				mock.Value.Verify();
		}

		public void VerifyAll()
		{
			foreach (var mock in _registeredMocks)
				mock.Value.VerifyAll();
		}

		private Mock GetMockForType(Type type)
		{
			if (_registeredMocks.Keys.Contains(type) == false)
			{
				var mockBehavior = GetMockBehaviorForType(type);
				var ctor = typeof(Mock<>)
					.MakeGenericType(new Type[] { type })
					.GetConstructor(new Type[] { typeof(MockBehavior) });
				_registeredMocks.Add(type, (Mock)ctor.Invoke(new object[] { mockBehavior }));
			}

			return _registeredMocks[type];
		}

		private MockBehavior GetMockBehaviorForType(Type type)
		{
			if (_mockBehaviors.Keys.Contains(type))
				return _mockBehaviors[type];

			return _defaultBehavior;
		}

		private readonly IDictionary<Type, Mock> _registeredMocks;
		private readonly IDictionary<Type, MockBehavior> _mockBehaviors;
		private readonly MockBehavior _defaultBehavior;
	}
}
