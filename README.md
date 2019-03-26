# Sample tests

Aspires to provide a simple comparison between pure unit tests and integration tests in the following scenario:
* there is a model representing incomming data
* there is a controller (in MVC or WebAPI sense)
* there is a non-trivial validator for the `Model` that the `Controller` works with
* the `Validator` decides whether a `Model` instance provided to the `Controller`'s `Method` is worth processing or not
