variable "subscription_id" {
  description = "Azure subscription in which to create resources."
  type        = string
}

variable "location" {
  description = "Azure region."
  type        = string
  default     = "eastus"
}

variable "resource_group_name" {
  description = "Resource group for OrderFlow development."
  type        = string
  default     = "rg-orderflow-dev"
}

variable "acr_name" {
  description = "Globally unique registry name: 5–50 lowercase letters or digits."
  type        = string

  validation {
    condition     = can(regex("^[a-z0-9]{5,50}$", var.acr_name))
    error_message = "Use 5–50 lowercase letters or digits."
  }
}